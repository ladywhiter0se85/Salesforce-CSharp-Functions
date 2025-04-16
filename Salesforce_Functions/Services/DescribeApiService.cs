using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Salesforce_Functions.Models.Responses;

namespace Salesforce_Functions.Services
{
    public class DescribeApiService
    {
        private ApiService _apiService;
        private ILogger _logger;

        public DescribeApiService(ILogger logger)
        {
            _apiService = new ApiService(logger);
            _logger = logger;
        }

        public async Task<ApiResponse<Dictionary<string, List<DescribeField>>>> GetDescribesAsync(List<string> sObjects)
        {
            _logger.LogInformation($"DescribeApiService: Get Request Started.");

            var sObjectDescribeFields = new Dictionary<string, List<DescribeField>>();
            var accessToken = await _apiService.GetAccessTokenAsync();
            foreach (var sObject in sObjects.Distinct())
            {
                _logger.LogInformation($"DescribeApiService: Requesting Descibe Data for {sObject}.");
                var initialResp = await _apiService.GetDescribeAsync(accessToken, sObject);

                //Take initial Describe request and get the Fields Array
                var json = JObject.Parse(initialResp);
                var fieldsArray = json["fields"] as JArray;
                if (fieldsArray == null)
                {
                    _logger.LogWarning($"No fields data found for SObject: {sObject}");
                    continue; // Skip to the next sObject
                }

                //Filter to desired Fields for Describe response
                var filteredFields = fieldsArray.Select(field => new DescribeField
                {
                    Name = field["name"]?.ToString() ?? string.Empty,
                    Label = field["label"]?.ToString() ?? string.Empty,
                    Type = field["type"]?.ToString() ?? string.Empty,
                    Nillable = field["nillable"]?.Value<bool>() ?? false,
                    Createable = field["createable"]?.Value<bool>() ?? false,
                    Updateable = field["updateable"]?.Value<bool>() ?? false,
                    ExternalId = field["externalId"]?.Value<bool>() ?? false,
                    Custom = field["custom"]?.Value<bool>() ?? false,
                    ReferenceTo = field["referenceTo"]?.ToObject<List<string>>() ?? new List<string>(),
                    RelationshipName = field["relationshipName"]?.ToString()
                }).ToList();

                sObjectDescribeFields[sObject] = filteredFields;
            }

            _logger.LogInformation($"DescribeApiService: Get Request Complete.");
            return new ApiResponse<Dictionary<string, List<DescribeField>>>(sObjectDescribeFields);
        }
    }
}