using Newtonsoft.Json;
using Salesforce_Functions.Configurations;

namespace Salesforce_Functions.Utilities
{
    public static class ConfigurationUtility
    {
        public static bool IsTestEnvironment { get; set; } = false;
        public static bool IsLocalEnvironment => Environment.GetEnvironmentVariable("RUN_ENVIRONMENT") == "LOCAL";

        public static ApiProperties GetApiProperties()
        {
            if (IsTestEnvironment)
            {
                return ReadApiPropertiesFromJson();
            }
            else
            {
                return SetApiPropertiesEnv();
            }
        }

        private static ApiProperties SetApiPropertiesEnv()
        {
            var key = GetRequiredEnvVar("ENCRYPTION_KEY");
            var iv = GetRequiredEnvVar("ENCRYPTION_IV");

            return new ApiProperties
            {
                EncryptionKey = key,
                EncryptionIV = iv,
                LoginUrl = GetRequiredEnvVar("SALESFORCE_LOGIN_URL"),
                DomainUrl = GetRequiredEnvVar("SALESFORCE_DOMAIN_URL"),
                Version = GetRequiredEnvVar("SALESFORCE_VERSION"),
                ClientId = CheckEncryption(key, iv, GetRequiredEnvVar("SALESFORCE_CLIENT_ID")),
                ClientSecret = CheckEncryption(key, iv, GetRequiredEnvVar("SALESFORCE_CLIENT_SECRET")),
                Username = CheckEncryption(key, iv, GetRequiredEnvVar("SALESFORCE_USERNAME")),
                Password = CheckEncryption(key, iv, GetRequiredEnvVar("SALESFORCE_PASSWORD")),
                SecretToken = CheckEncryption(key, iv, GetRequiredEnvVar("SALESFORCE_SECRET_TOKEN"))
            };
        }

        private static string GetRequiredEnvVar(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Missing required environment variable: {key}");
            }
            return value;
        }


        private static string CheckEncryption(string key, string iv, string apiProperty)
        {
            if (IsLocalEnvironment)
            {
                return EncryptionUtility.Decrypt(key, iv, apiProperty);
            }
            return apiProperty;
        }

        private static ApiProperties ReadApiPropertiesFromJson()
        {

            // Check if the JSON file exists before trying to read it
            if (!File.Exists("Resources/apiProperties.json"))
            {
                throw new FileNotFoundException("API properties file not found.");
            }

            var json = File.ReadAllText("Resources/apiProperties.json");
            var apiProperties = JsonConvert.DeserializeObject<ApiProperties>(json);
            var key = apiProperties!.EncryptionKey;
            var iv = apiProperties.EncryptionIV;

            // Decryption Sensitive Data
            apiProperties.ClientId = EncryptionUtility.Decrypt(key, iv, apiProperties.ClientId);
            apiProperties.ClientSecret = EncryptionUtility.Decrypt(key, iv, apiProperties.ClientSecret);
            apiProperties.Username = EncryptionUtility.Decrypt(key, iv, apiProperties.Username);
            apiProperties.Password = EncryptionUtility.Decrypt(key, iv, apiProperties.Password);
            apiProperties.SecretToken = EncryptionUtility.Decrypt(key, iv, apiProperties.SecretToken);

            return apiProperties;
        }
    }
}