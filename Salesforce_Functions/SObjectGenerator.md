# SObject Generator – Explanation and Usage

Instead of manually creating the Salesforce SObject classes required by the Function App endpoints, this project includes a Bash script that automates the generation process. By supplying the necessary login details and specifying an SObject type, the script will generate the corresponding class file in the `Models` folder.

---

## 🔧 How to Run

The `generate-sobject.sh` script is located in the `Salesforce_Functions` folder.

### Steps

1. Open a terminal and navigate to the project folder:

   ```bash
   cd Salesforce_Functions
   ```

2. Run the script with the required arguments:

   ```bash
   ./generate-sobject.sh --username {USERNAME} --password {PASSWORDSECRETOKEN} --client-id {CLIENTID} --client-secret {CLIENTSECRET} --sobject {SOBJECT}
   ```

   Replace the placeholders:
   - `{USERNAME}` – Salesforce username  
   - `{PASSWORDSECRETOKEN}` – password + security token  
   - `{CLIENTID}` – Salesforce connected app client ID  
   - `{CLIENTSECRET}` – Salesforce connected app client secret  
   - `{SOBJECT}` – e.g., `Account`, `Contact`, etc.

3. After execution, the generated `SObject.cs` file will appear under:

   ```txt
   Salesforce_Functions/Models
   ```

---

### ⚙️ What the Script Does

1. **Authenticates** with Salesforce using `https://login.salesforce.com` to obtain an access token.
2. Retrieves the **instance URL** for the authenticated Salesforce org.
3. Checks for the existence of `Attributes.cs` (a shared dependency for all SObjects) and creates it if missing.
4. Creates the `SObject.cs` file.
5. Retrieves the **Describe** metadata for the specified SObject, focusing on the `fields` array, extracting:
   - `name`
   - `type`
   - `referenceTo` (for lookup/relationship fields)
6. Maps field types to C# types:
   - Common types like `string`, `boolean`, `int`, etc.
   - Reference fields and unknown types default to `object?`
7. Logs any related SObjects (e.g., lookup relationships) that weren't fully generated to:

   ```txt
   Salesforce_Functions/Resources/missing_references.txt
   ```

   These can be manually created later for improved type safety.

8. At the end of execution, the script displays any additional SObjects referenced that can optionally be created.
