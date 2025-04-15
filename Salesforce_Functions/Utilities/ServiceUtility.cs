using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Salesforce_Functions.Exceptions;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Services;

namespace Salesforce_Functions.Utilities
{
    public class ServiceUtility
    {
        public static async Task<ApiResponse<T>> GetSObjectResponseAsync<T, TQueryResult>(ApiService apiService, ILogger logger, string sObjectName, string query, Func<TQueryResult, T> convertResponse)
        {

            logger.LogInformation($"{sObjectName}ApiService: Get Request Started.");
            return await ProcessErrorsAsync(logger, async () =>
            {
                var accessToken = await apiService.GetAccessTokenAsync();
                var initialResp = await apiService.GetRequestAsync(accessToken, sObjectName, query);

                var json = JObject.Parse(initialResp);
                var records = json["records"]?.ToString();

                if (records is null)
                {
                    logger.LogWarning($"{sObjectName}ApiService: No records found.");
                    throw new ApiResponseException<string>(new ApiResponse<string>(HttpStatusCode.NotFound, "No records found."));
                }

                var resp = JsonConvert.DeserializeObject<TQueryResult>(records);
                var converted = convertResponse(resp!);

                logger.LogInformation($"{sObjectName}ApiService: Get Request Complete.");
                return converted;
            });
        }

        public static async Task<ApiResponse<List<OperationResponse>>> ProcessSObjectOperationAsync(ApiService apiService, ILogger logger, string sObjectName, HttpMethod method, string body, string? externalField = null)
        {
            logger.LogInformation($"{sObjectName}ApiService: {method.ToString} Request Started.");
            return await ProcessErrorsAsync(logger, async () =>
            {
                var accessToken = await apiService.GetAccessTokenAsync();

                string initialResp = method switch
                {
                    var m when m == HttpMethod.Post => await apiService.PostRequesAsynct(accessToken, sObjectName, body),
                    var m when m == HttpMethod.Patch => await apiService.PatchRequestAsync(accessToken, sObjectName, body),
                    var m when m == HttpMethod.Put => await apiService.UpsertRequestAsync(accessToken, sObjectName, body, externalField!),
                    _ => throw new NotSupportedException($"HTTP method {method} is not supported.")
                };

                logger.LogInformation($"{sObjectName}ApiService: {method.ToString} Request Complete.");
                return JsonConvert.DeserializeObject<List<OperationResponse>>(initialResp)!;
            });
        }

        public static string BuildWhereFilter(string filter, bool isAnd = true)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return string.Empty;

            var conditions = filter
                .Split('&', StringSplitOptions.RemoveEmptyEntries)
                .Select(pair => pair.Split(':', 2))
                .Where(kv => kv.Length == 2)
                .Select(kv =>
                {
                    var column = kv[0].Trim();
                    var values = kv[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(v => $"'{v.Trim()}'")
                                      .ToList();

                    return values.Count > 1
                        ? $"{column} IN ({string.Join(", ", values)})"
                        : $"{column} = {values.First()}";
                });

            var joinOperator = isAnd ? " AND " : " OR ";
            return conditions.Any() ? string.Join(joinOperator, conditions) : string.Empty;
        }

        private static async Task<ApiResponse<T>> ProcessErrorsAsync<T>(ILogger logger, Func<Task<T>> action)
        {
            try
            {
                var result = await action();
                return new ApiResponse<T>(result);
            }
            catch (ApiResponseException<string> e)
            {
                return new ApiResponse<T>(e.ApiResponse.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new ApiResponse<T>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}