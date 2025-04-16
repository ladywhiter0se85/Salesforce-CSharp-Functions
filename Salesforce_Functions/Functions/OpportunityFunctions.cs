using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Salesforce_Functions.Exceptions;
using Salesforce_Functions.Models;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Models.Validation;
using Salesforce_Functions.Services;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions
{
    public class OpportunityFunctions
    {
        private readonly ILogger<OpportunityFunctions> _logger;
        private readonly OpportunityApiService _opportunityApiService;

        public OpportunityFunctions(ILogger<OpportunityFunctions> logger)
        {
            _logger = logger;
            _opportunityApiService = new OpportunityApiService(logger);
        }

        [Function("GetOpportunities")]
        [OpenApiOperation(operationId: "GetOpportunities", Summary = "Retrieve a list of Salesforce Opportunities", Description = "Retrieves all available Salesforce opportunities with basic details.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Opportunity>), Description = "Successful response with list of Salesforce Opportunities", Example = typeof(OpportunitiesOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetOpportunities([HttpTrigger(AuthorizationLevel.Function, "get", Route = "opportunities")] HttpRequestData req)
        {
            _logger.LogInformation("OpportunityFunctions: Get All Salesforce Opportunities Request");
            var opportunitiesResponse = await _opportunityApiService.GetAllOpportunitiesAsync();
            var response = await ResponseUtility.FromApiResponse(req, opportunitiesResponse);
            _logger.LogInformation("OpportunityFunctions: Get All Salesforce Opportunities Request Complete");
            return response;
        }

        [Function("GetOpportunityById")]
        [OpenApiOperation(operationId: "GetOpportunityById", Summary = "Retrieve a Salesforce Opportunity by Id", Description = "Retrieves a Salesforce opportunity by id with basic details.")]
        [OpenApiParameter(name: "opportunityId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Salesforce Opportunity Id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Opportunity), Description = "Successful response with a Salesforce Opportunity", Example = typeof(OpportunityOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetOpportunityById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "opportunities/id/{opportunityId}")] HttpRequestData req, string opportunityId)
        {
            _logger.LogInformation("OpportunityFunctions: Get Salesforce Opportunity By Id Request");
            try
            {
                var id = ParameterValidation.Validate(opportunityId, "opportunityId");
                var opportunitiesResponse = await _opportunityApiService.GetOpportunityByIdAsync(id);
                var response = await ResponseUtility.FromApiResponse(req, opportunitiesResponse);
                _logger.LogInformation("OpportunityFunctions: Get Salesforce Opportunity By Id Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("GetOpportunitiesByFilter")]
        [OpenApiOperation(operationId: "GetOpportunitiesByFilter", Summary = "Retrieve a Salesforce Opportunities by Filter", Description = "Retrieves Salesforce opportunities by filter with basic details.")]
        [OpenApiParameter(name: "where", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Where Condition Filters")]
        [OpenApiParameter(name: "isAnd", In = ParameterLocation.Query, Required = true, Type = typeof(bool), Description = "Type of Condition Check AND/OR")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Opportunity>), Description = "Successful response with list of Salesforce Opportunities", Example = typeof(OpportunitiesOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetOpportunitiesByFilter([HttpTrigger(AuthorizationLevel.Function, "get", Route = "opportunities/filter/{where}")] HttpRequestData req, string where, bool? isAnd)
        {
            _logger.LogInformation("OpportunityFunctions: Get Salesforce Opportunities By Filter Request");
            try
            {
                var filter = ParameterValidation.Validate(where, "where");
                var cond = ParameterValidation.Validate(isAnd, "isAnd");
                var opportunitiesResponse = await _opportunityApiService.GetOpportunitiesByFilterAsync(filter, isAnd ?? false);
                var response = await ResponseUtility.FromApiResponse(req, opportunitiesResponse);
                _logger.LogInformation("OpportunityFunctions: Get Salesforce Opportunities By Filter Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PostOpportunities")]
        [OpenApiOperation(operationId: "PostOpportunities", Summary = "Create Salesforce Opportunities", Description = "Creates Salesforce opportunities.")]
        [OpenApiRequestBody("application/json", typeof(List<Opportunity>), Required = true, Description = "List of Opportunity objects to create", Example = typeof(OpportunitiesOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PostOpportunities([HttpTrigger(AuthorizationLevel.Function, "post", Route = "opportunities")] HttpRequestData req)
        {
            _logger.LogInformation("OpportunityFunctions: Post Salesforce Opportunities Request");
            try
            {
                var body = await BodyValidation.ValidateAsync(req.Body);
                var opportunitiesResponse = await _opportunityApiService.PostOpportunitiesAsync(body);
                var response = await ResponseUtility.FromApiResponse(req, opportunitiesResponse);
                _logger.LogInformation("OpportunityFunctions: Post Salesforce Opportunities Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PatchOpportunities")]
        [OpenApiOperation(operationId: "PatchOpportunities", Summary = "Updates Salesforce Opportunities", Description = "Updates Salesforce opportunities.")]
        [OpenApiRequestBody("application/json", typeof(List<Opportunity>), Required = true, Description = "List of Opportunity objects to update", Example = typeof(OpportunitiesOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PatchOpportunities([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "opportunities")] HttpRequestData req)
        {
            _logger.LogInformation("OpportunityFunctions: Patch Salesforce Opportunities Request");
            try
            {
                var body = await BodyValidation.ValidateAndCheckIdsAsync(req.Body);
                var opportunitiesResponse = await _opportunityApiService.PatchOpportunitiesAsync(body);
                var response = await ResponseUtility.FromApiResponse(req, opportunitiesResponse);
                _logger.LogInformation("OpportunityFunctions: Patch Salesforce Opportunities Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PutOpportunities")]
        [OpenApiOperation(operationId: "PutOpportunities", Summary = "Upsert Salesforce Opportunities", Description = "Upsert Salesforce opportunities.")]
        [OpenApiParameter(name: "externalField", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Salesforce External field used for upsert.")]
        [OpenApiRequestBody("application/json", typeof(List<Opportunity>), Required = true, Description = "List of Opportunity objects to upsert", Example = typeof(OpportunitiesOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PutOpportunities([HttpTrigger(AuthorizationLevel.Function, "put", Route = "opportunities/external/{externalField}")] HttpRequestData req, string externalField)
        {
            _logger.LogInformation("OpportunityFunctions: Put Salesforce Opportunities Request");
            try
            {
                var field = ParameterValidation.Validate(externalField, "externalField");
                var body = await BodyValidation.ValidateAsync(req.Body);
                var opportunitiesResponse = await _opportunityApiService.UpsertOpportunitiesAsync(body, field);
                var response = await ResponseUtility.FromApiResponse(req, opportunitiesResponse);
                _logger.LogInformation("OpportunityFunctions: Put Salesforce Opportunities Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }
    }
}
