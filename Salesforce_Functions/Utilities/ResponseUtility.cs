using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Salesforce_Functions.Models.Responses;
using System.Net;

namespace Salesforce_Functions.Utilities
{
    public class ResponseUtility
    {
        public static async Task<HttpResponseData> FromApiResponse<T>(HttpRequestData req, ApiResponse<T> apiResponse)
        {
            var response = req.CreateResponse(apiResponse.StatusCode);
            response.Headers.Add("Content-Type", "application/json");
            var resp = apiResponse.StatusCode == HttpStatusCode.OK ? SetJsonSettings(apiResponse.Data) : apiResponse.Message;
            await response.WriteStringAsync(resp!);
            return response;
        }

        public static string SetJsonSettings<T>(T json)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore, // Ignore null properties
                DefaultValueHandling = DefaultValueHandling.Ignore, // Ignore default values (0, false, etc.)
                Formatting = Formatting.Indented, // Optional: pretty print JSON
                Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() } // Optional: handle enum conversion
            };

            return JsonConvert.SerializeObject(json, settings);
        }

        public static string SetJsonSettingsCompact<T>(T json)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore, // Ignore null properties
                DefaultValueHandling = DefaultValueHandling.Ignore, // Ignore default values (0, false, etc.)
                Formatting = Formatting.None, // Keep json compact
                Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() } // Optional: handle enum conversion
            };

            return JsonConvert.SerializeObject(json, settings);
        }

        public static string ReadFileToCompactJson<T>(string jsonExample)
        {
            var jsonText = ReadFile(jsonExample);
            var processedItem = SetJsonSettingsCompact(JsonConvert.DeserializeObject<T>(jsonText));
            processedItem = processedItem.Replace("\r\n", "").Replace("\n", "");
            return processedItem;
        }

        public static string ReadFile(string filePath)
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var localFilePath = outputDirectory + filePath;

                return File.ReadAllText(localFilePath);
            }
        }
    }
}