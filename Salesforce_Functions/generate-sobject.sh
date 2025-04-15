#!/bin/bash
set -e # Exit immediately if a command fails

# Script Overview:
# This script generates C# classes for Salesforce SObjects by querying Salesforce's metadata API.
# It authenticates using the password grant type and retrieves metadata for a specified SObject.
# The generated classes include properties based on the fields of the SObject and a shared Attributes class.

# NOTE: Any subclasses will need to be added with this script and then manually updated to main class (i.e Account -> Address for BillingAddress)

# Function to show help
show_help() {
	echo "Usage: $0 --username USERNAME --password PASSWORD --client-id CLIENT_ID --client-secret CLIENT_SECRET --sobject SOBJECT"
	echo ""
	echo "  --username        Salesforce username"
	echo "  --password        Salesforce password + security token"
	echo "  --client-id       Salesforce Connected App client ID"
	echo "  --client-secret   Salesforce Connected App client secret"
	echo "  --sobject         Salesforce object to describe (e.g., Account)"
	exit 1
}

# Log function
log() {
	echo "$(date +'%Y-%m-%d %H:%M:%S') - $1"
}

# Function to create class file
create_class_file() {
	local class_file=$1
	local class_name=$2
	cat <<EOF >"$class_file" # Use the full file path here
namespace $NAMESPACE
{
	public class $class_name
	{
EOF
	log "$class_name class created at $class_file"
}

# Function to get the C# type based on the Salesforce field type
get_csharp_type() {
	case "$1" in
	"picklist" | "textarea" | "string" | "email" | "phone" | "url" | "id") echo "string" ;;
	"boolean") echo "bool" ;;
	"double" | "currency") echo "double" ;;
	"int") echo "int" ;;
	"datetime" | "date") echo "DateTime" ;;
	*)
		echo "object" # Default to object for unknown types

		# Get the properly capitalized type
		CAPITALIZED_REF=$(capitalize_type "$1")

		# Only append if it's not already in the file (exact match)
		if ! grep -Fxq "$CAPITALIZED_REF" "$MISSING_REFS_FILE"; then
			echo "$CAPITALIZED_REF" >>"$MISSING_REFS_FILE"
		fi
		;;
	esac
}

# Function to capitalize the first letter of a type
capitalize_type() {
	local type="$1"
	echo "${type^}" # Capitalizes the first letter
}

# Function to handle reference fields and add missing references
process_references() {
	local field=$1

	# Extract the referenceTo part (assuming it's the 3rd part in the description)
	REF_OBJECTS=$(echo "$field" | cut -d: -f3-)
	REF_OBJECTS=$(echo "$REF_OBJECTS" | tr -d '[]"')

	IFS=',' read -r -a REF_OBJECTS_ARRAY <<<"$REF_OBJECTS"

	for REF_OBJECT in "${REF_OBJECTS_ARRAY[@]}"; do
		REF_OBJECT=$(echo "$REF_OBJECT" | tr -d '"') # Clean up extra characters

		# Extract the name (field name) from the field description
		NAME=$(echo "$field" | cut -d: -f1)

		# Check if the reference is the same as the SObject
		if [[ "$REF_OBJECT" == "$SOBJECT" ]]; then
			# If it's the same, use the reference object as it is
			echo "        public $REF_OBJECT? $NAME { get; set; }" >>"$SOBJECT_FILE"
		else
			# If it's not the same, use object as the reference type
			echo "        public object? $NAME { get; set; }" >>"$SOBJECT_FILE"

			# Log the missing reference
			if ! grep -q "$REF_OBJECT" "$MISSING_REFS_FILE"; then
				# Only log the reference if it's not already listed
				echo "$REF_OBJECT" >>"$MISSING_REFS_FILE"
			fi
		fi
	done
}

# Function to remove a reference from the missing references file after it has been added
remove_reference() {
	local ref=$1
	# Remove the reference from the missing references file
	sed -i "/$ref/d" "$MISSING_REFS_FILE"
}

# Function to display missing references if any
display_missing_references() {
	# Check if there are any missing references
	if [ -s "$MISSING_REFS_FILE" ]; then
		echo "Missing References:"
		# List unique references only (use sort -u to remove duplicates)
		sort -u "$MISSING_REFS_FILE"
	else
		echo "No missing references to be added."
	fi
}

# Shared Variables
LOGIN_URL="https://login.salesforce.com"
API_VERSION="58.0"
PROJECT_DIR=$(pwd)
NAMESPACE=$(basename "$PROJECT_DIR") # Assuming the folder name is the desired namespace

# Replace dashes with underscores for the namespace
NAMESPACE=${NAMESPACE//-/_}

# File to log missing references
MISSING_REFS_FILE="Resources/missing_references.txt"

# Ensure the directory exists
mkdir -p "$(dirname "$MISSING_REFS_FILE")"

# Create the file if it doesn't exist
if [ ! -f "$MISSING_REFS_FILE" ]; then
    touch "$MISSING_REFS_FILE"
fi

# Parse arguments and check required parameters
while [[ $# -gt 0 ]]; do
	case "$1" in
	--username)
		USERNAME="$2"
		shift 2
		;;
	--password)
		PASSWORD="$2"
		shift 2
		;;
	--client-id)
		CLIENT_ID="$2"
		shift 2
		;;
	--client-secret)
		CLIENT_SECRET="$2"
		shift 2
		;;
	--sobject)
		SOBJECT="$2"
		shift 2
		;;
	--login-url)
		LOGIN_URL="$2"
		shift 2
		;;
	-h | --help) show_help ;;
	*)
		echo "Unknown argument: $1"
		show_help
		;;
	esac
done

# Check required parameters
if [[ -z "$USERNAME" || -z "$PASSWORD" || -z "$CLIENT_ID" || -z "$CLIENT_SECRET" || -z "$SOBJECT" ]]; then
	echo "❌ Missing required parameters."
	show_help
fi

log "Starting script for SObject: $SOBJECT"

# Request access token
log "🔐 Requesting access token..."
RESPONSE=$(curl -k -X POST "${LOGIN_URL}/services/oauth2/token" -d "grant_type=password" -d "client_id=${CLIENT_ID}" -d "client_secret=${CLIENT_SECRET}" -d "username=${USERNAME}" -d "password=${PASSWORD}")
ACCESS_TOKEN=$(echo "$RESPONSE" | grep -o '"access_token":"[^"]*' | sed 's/"access_token":"//')
INSTANCE_URL=$(echo "$RESPONSE" | grep -o '"instance_url":"[^"]*' | sed 's/"instance_url":"//')

# Check if we got the token
if [[ -z "$ACCESS_TOKEN" || "$ACCESS_TOKEN" == "null" ]]; then
	log "❌ Authentication failed."
	exit 1
fi

log "✅ Authenticated. Access token acquired."

# Creating Attributes class if not exists
ATTRIB_FILE="Models/Attributes.cs"
if [ -f "$ATTRIB_FILE" ]; then
	# If it exists, show a green check mark
	echo "✅ Attributes class already exists. No need to create it again."
else
	# If it doesn't exist, create the Attributes class
	create_class_file "$ATTRIB_FILE" "Attributes"
	echo "        public string Type { get; set; }" >>"$ATTRIB_FILE"
	echo "        public string Url { get; set; }" >>"$ATTRIB_FILE"
	echo "    }" >>"$ATTRIB_FILE"
	echo "}" >>"$ATTRIB_FILE"
	echo "✅ Attributes class created."
fi

# Creating SObject class
SOBJECT_FILE="Models/$SOBJECT.cs"
create_class_file "$SOBJECT_FILE" "$SOBJECT"

# Add the Attributes object to the SObject class
echo "        public Attributes Attributes { get; set; } = new Attributes { Type = \"$SOBJECT\" };" >>"$SOBJECT_FILE"

# Fetching fields from Salesforce
log "🔄 Fetching fields for $SOBJECT..."
FIELDS_JSON=$(curl -k -X GET "${INSTANCE_URL}/services/data/v${API_VERSION}/sobjects/${SOBJECT}/describe" \
	-H "Authorization: Bearer ${ACCESS_TOKEN}" \
	-H "Content-Type: application/json")
FIELDS=$(echo "$FIELDS_JSON" | jq -r '.fields[] | "\(.name):\(.type):\(.referenceTo // "")"')
echo "✅ Successful fetching of $SOBJECT fields."

# Initialize the flag to track if JsonProperty needs to be added
NEEDS_JSON_IMPORT=false

# Define C# types in an array for easier checking
C_SHARP_TYPES=("string" "bool" "double" "int" "DateTime")

# Processing fields
for FIELD in $FIELDS; do
	NAME=$(echo "$FIELD" | cut -d: -f1)
	TYPE=$(echo "$FIELD" | cut -d: -f2)
	REF_OBJECTS=$(echo "$FIELD" | cut -d: -f3-)

	# Determine the C# property type based on the field type
	if [[ "$TYPE" == "reference" ]]; then
		process_references "$FIELD"
	else
		# Get the C# type by calling the function
		CSHARP_TYPE=$(get_csharp_type "$TYPE")

		# Check if the property name is the same as the SObject name
		if [[ "$NAME" == "$SOBJECT" ]]; then
			# If so, append "Object" to the property name to avoid a naming conflict
			NAME="${SOBJECT}Object"

			# Set the flag to true because JsonProperty will be used
			NEEDS_JSON_IMPORT=true

			# Add the JsonProperty attribute for proper deserialization
			echo "        [JsonProperty(\"$SOBJECT\")]" >>"$SOBJECT_FILE"
		fi

		echo "        public $CSHARP_TYPE? $NAME { get; set; }" >>"$SOBJECT_FILE"
	fi

done

# If the flag is true, add the Newtonsoft.Json import at the top of the file
if [[ "$NEEDS_JSON_IMPORT" == true ]]; then
	# Check if the import for Newtonsoft.Json is already present
	if ! grep -q "using Newtonsoft.Json;" "$SOBJECT_FILE"; then
		# If the import is missing, add it at the top of the file
		sed -i '1i using Newtonsoft.Json;' "$SOBJECT_FILE"
	fi
fi

# Close the class and namespace (not yet adding fields)
echo "    }" >>"$SOBJECT_FILE"
echo "}" >>"$SOBJECT_FILE"

echo "✅ $SOBJECT class created."

# Afrer SObject Processed remove from Missing References txt file if exists
remove_reference $SOBJECT
# Then display any additional references that could be manually added and are currently set as object?
display_missing_references
