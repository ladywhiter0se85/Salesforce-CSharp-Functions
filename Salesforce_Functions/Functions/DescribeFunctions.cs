using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Salesforce_Functions.Exceptions;
using Salesforce_Functions.Models;
using Salesforce_Functions.Models.Validation;
using Salesforce_Functions.Services;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions
{
    public class DescribeFunctions
    {
        private readonly ILogger<DescribeFunctions> _logger;
        private readonly DescribeApiService _describeApiService;

        public DescribeFunctions(ILogger<DescribeFunctions> logger)
        {
            _logger = logger;
            _describeApiService = new DescribeApiService(logger);
        }

        [Function("GetDescribes")]
        [OpenApiOperation(operationId: "GetDescribes", Summary = "Retrieve a Salesforce Describes", Description = "Retrieves Salesforce describes with basic details.")]
        [OpenApiParameter(name: "sObjects", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Comma separated list of SObjects Names")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Dictionary<string, List<DescribeField>>), Description = "Successful response with list of Salesforce Describes", Example = typeof(DescribesOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetDescribes([HttpTrigger(AuthorizationLevel.Function, "get", Route = "describes")] HttpRequestData req, string? sObjects)
        {
            _logger.LogInformation("DescribeFunctions: Get Salesforce Describes Request");
            try
            {
                var sObjectParam = ParameterValidation.Validate(sObjects, "sObjects");
                var sObjectList = sObjectParam?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList() ?? new List<string>();
                var describesResponse = await _describeApiService.GetDescribesAsync(sObjectList);
                var response = await ResponseUtility.FromApiResponse(req, describesResponse);
                _logger.LogInformation("DescribeFunctions: Get Salesforce Describes Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }
    }
}
