using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Salesforce_Functions.Configurations;
using Salesforce_Functions.Exceptions;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Services
{
    public class ApiService
    {
        private ApiProperties _apiProperties;
        private ILogger _logger;
        private string _requestURL;

        public ApiService(ILogger logger)
        {
            _apiProperties = ConfigurationUtility.GetApiProperties();
            _logger = logger;
            _requestURL = $"{_apiProperties.DomainUrl}/services/data/v{_apiProperties.Version}";
        }

        public async Task<string> GetAccessTokenAsync()
        {
            _logger.LogInformation("Requesting Salesforce Access Token.");
            using (HttpClient client = new HttpClient())
            {
                var tokenUrl = _apiProperties.LoginUrl + "/services/oauth2/token";

                // Set up the request headers
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var formData = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", _apiProperties.ClientId),
                new KeyValuePair<string, string>("client_secret", _apiProperties.ClientSecret),
                new KeyValuePair<string, string>("username", _apiProperties.Username),
                new KeyValuePair<string, string>("password", _apiProperties.Password + _apiProperties.SecretToken)
            });

                HttpResponseMessage response = await client.PostAsync(tokenUrl, formData);
                var responseContent = await ValidateResponse(response);
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                _logger.LogInformation("Request for Salesforce Access Token complete.");
                return tokenResponse!.AccessToken;
            }
        }

        public async Task<string> GetQueryAsync(string accessToken, string sObjectName, string query)
        {
            _logger.LogInformation($"Requesting Salesforce Object: {sObjectName}.");

            string url = _requestURL + $"/query/?q={query}";
            var responseContent = await GetRequestAsync(accessToken, url);

            _logger.LogInformation($"Salesforce Object: {sObjectName} request complete.");
            return responseContent;
        }

        public async Task<string> GetDescribeAsync(string accessToken, string sObjectName)
        {
            _logger.LogInformation($"Requesting Salesforce Describe for: {sObjectName}.");

            string url = _requestURL + $"/sobjects/{sObjectName}/describe";
            var responseContent = await GetRequestAsync(accessToken, url);

            _logger.LogInformation($"Salesforce Describe for: {sObjectName} request complete.");
            return responseContent;
        }

        public async Task<string> PostRequesAsync(string accessToken, string sObjectName, string data)
        {
            _logger.LogInformation($"Creating Salesforce Objects: {sObjectName}.");
            var responseContent = await GetOperationQuestionAsync(accessToken, data, HttpMethod.Post);
            _logger.LogInformation($"Salesforce Objects: {sObjectName} creation complete.");
            return responseContent;
        }

        public async Task<string> PatchRequestAsync(string accessToken, string sObjectName, string data)
        {
            _logger.LogInformation($"Updating Salesforce Objects: {sObjectName}.");
            var responseContent = await GetOperationQuestionAsync(accessToken, data, HttpMethod.Patch);
            _logger.LogInformation($"Salesforce Objects: {sObjectName} update complete.");
            return responseContent;
        }

        public async Task<string> UpsertRequestAsync(string accessToken, string sObjectName, string data, string externalField)
        {
            _logger.LogInformation($"Upserting Salesforce Objects: {sObjectName}.");
            var responseContent = await GetOperationQuestionAsync(accessToken, data, HttpMethod.Patch, sObjectName, externalField);
            _logger.LogInformation($"Salesforce Objects: {sObjectName} upsert complete.");
            return responseContent;
        }

        private async Task<string> GetRequestAsync(string accessToken, string url)
        {
            var httpClient = BuildHttpClient(accessToken);

            // Send the GET request
            var response = await httpClient.GetAsync(url);
            return await ValidateResponse(response);
        }

        private async Task<string> GetOperationQuestionAsync(string accessToken, string data, HttpMethod method, string? sObjectName = null, string? externalField = null)
        {
            var url = _requestURL + "/composite/sobjects";
            if (externalField != null && !externalField.IsEmpty())
            {
                url += "/" + sObjectName + "/" + externalField;
            }
            var httpClient = BuildHttpClient(accessToken);
            var content = new StringContent(WrapDataForComposite(data), Encoding.UTF8, "application/json");

            // Send the Operation request
            var response = method == HttpMethod.Post ? await httpClient.PostAsync(url, content) : await httpClient.PatchAsync(url, content);
            return await ValidateResponse(response);
        }

        private HttpClient BuildHttpClient(string accessToken)
        {
            var client = new HttpClient();
            // Set the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private async Task<string> ValidateResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                var msg = $"Request failed with Status Code: '{response.StatusCode}'. Error Message: {resp}";
                _logger.LogError(msg);
                throw new ApiResponseException<string>(new ApiResponse<string>(response.StatusCode, resp));
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            // Salesforce will sometimes return a 200 OK but still have errors
            // Validate whether the response content contains the error(s) array
            var json = JToken.Parse(responseContent);
            if (json is JArray jsonArray)
            {
                bool hasErrors = jsonArray.Any(x =>
                (x["success"]?.Value<bool>() == false) ||
                (x["errors"] is JArray errors && errors.Count > 0)
                );

                if (hasErrors)
                {
                    _logger.LogError("Salesforce logical error: {Response}", responseContent);
                    throw new ApiResponseException<string>(new ApiResponse<string>(HttpStatusCode.BadRequest, responseContent));
                }
            }

            return responseContent;
        }

        private string WrapDataForComposite(string data)
        {
            return $"{{\"allOrNone\": false, \"records\": {data}}}";
        }
    }
}