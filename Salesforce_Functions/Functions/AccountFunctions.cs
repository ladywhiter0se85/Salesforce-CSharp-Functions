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
    public class AccountFunctions
    {
        private readonly ILogger<AccountFunctions> _logger;
        private readonly AccountApiService _accountApiService;

        public AccountFunctions(ILogger<AccountFunctions> logger)
        {
            _logger = logger;
            _accountApiService = new AccountApiService(logger);
        }

        [Function("GetAccounts")]
        [OpenApiOperation(operationId: "GetAccounts", Summary = "Retrieve a list of Salesforce Accounts", Description = "Retrieves all available Salesforce accounts with basic details.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Account>), Description = "Successful response with list of Salesforce Accounts", Example = typeof(AccountsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetAccounts([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts")] HttpRequestData req)
        {
            _logger.LogInformation("AccountFunctions: Get All Salesforce Accounts Request");
            var accountsResponse = await _accountApiService.GetAllAccountsAsync();
            var response = await ResponseUtility.FromApiResponse(req, accountsResponse);
            _logger.LogInformation("AccountFunctions: Get All Salesforce Accounts Request Complete");
            return response;
        }

        [Function("GetAccountById")]
        [OpenApiOperation(operationId: "GetAccountById", Summary = "Retrieve a Salesforce Account by Id", Description = "Retrieves a Salesforce account by id with basic details.")]
        [OpenApiParameter(name: "accountId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Salesforce Account Id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Account), Description = "Successful response with a Salesforce Account", Example = typeof(AccountOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetAccountById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/id/{accountId}")] HttpRequestData req, string accountId)
        {
            _logger.LogInformation("AccountFunctions: Get Salesforce Account By Id Request");
            try
            {
                var id = ParameterValidation.Validate(accountId, "accountId");
                var accountsResponse = await _accountApiService.GetAccountByIdAsync(id);
                var response = await ResponseUtility.FromApiResponse(req, accountsResponse);
                _logger.LogInformation("AccountFunctions: Get Salesforce Account By Id Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("GetAccountsByFilter")]
        [OpenApiOperation(operationId: "GetAccountsByFilter", Summary = "Retrieve a Salesforce Accounts by Filter", Description = "Retrieves Salesforce accounts by filter with basic details.")]
        [OpenApiParameter(name: "where", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Where Condition Filters")]
        [OpenApiParameter(name: "isAnd", In = ParameterLocation.Query, Required = true, Type = typeof(bool), Description = "Type of Condition Check AND/OR")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Account>), Description = "Successful response with list of Salesforce Accounts", Example = typeof(AccountsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetAccountsByFilter([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/filter/{where}")] HttpRequestData req, string where, bool? isAnd)
        {
            _logger.LogInformation("AccountFunctions: Get Salesforce Accounts By Filter Request");
            try
            {
                var filter = ParameterValidation.Validate(where, "where");
                var cond = ParameterValidation.Validate(isAnd, "isAnd");
                var accountsResponse = await _accountApiService.GetAccountsByFilterAsync(filter, isAnd ?? false);
                var response = await ResponseUtility.FromApiResponse(req, accountsResponse);
                _logger.LogInformation("AccountFunctions: Get Salesforce Accounts By Filter Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PostAccounts")]
        [OpenApiOperation(operationId: "PostAccounts", Summary = "Create Salesforce Accounts", Description = "Creates Salesforce accounts.")]
        [OpenApiRequestBody("application/json", typeof(List<Account>), Required = true, Description = "List of Account objects to create", Example = typeof(AccountsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PostAccounts([HttpTrigger(AuthorizationLevel.Function, "post", Route = "accounts")] HttpRequestData req)
        {
            _logger.LogInformation("AccountFunctions: Post Salesforce Accounts Request");
            try
            {
                var body = await BodyValidation.ValidateAsync(req.Body);
                var accountsResponse = await _accountApiService.PostAccountsAsync(body);
                var response = await ResponseUtility.FromApiResponse(req, accountsResponse);
                _logger.LogInformation("AccountFunctions: Post Salesforce Accounts Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PatchAccounts")]
        [OpenApiOperation(operationId: "PatchAccounts", Summary = "Updates Salesforce Accounts", Description = "Updates Salesforce accounts.")]
        [OpenApiRequestBody("application/json", typeof(List<Account>), Required = true, Description = "List of Account objects to update", Example = typeof(AccountsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PatchAccounts([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "accounts")] HttpRequestData req)
        {
            _logger.LogInformation("AccountFunctions: Patch Salesforce Accounts Request");
            try
            {
                var body = await BodyValidation.ValidateAndCheckIdsAsync(req.Body);
                var accountsResponse = await _accountApiService.PatchAccountsAsync(body);
                var response = await ResponseUtility.FromApiResponse(req, accountsResponse);
                _logger.LogInformation("AccountFunctions: Patch Salesforce Accounts Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PutAccounts")]
        [OpenApiOperation(operationId: "PutAccounts", Summary = "Upsert Salesforce Accounts", Description = "Upsert Salesforce accounts.")]
        [OpenApiParameter(name: "externalField", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Salesforce External field used for upsert.")]
        [OpenApiRequestBody("application/json", typeof(List<Account>), Required = true, Description = "List of Account objects to upsert", Example = typeof(AccountsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PutAccounts([HttpTrigger(AuthorizationLevel.Function, "put", Route = "accounts/external/{externalField}")] HttpRequestData req, string externalField)
        {
            _logger.LogInformation("AccountFunctions: Put Salesforce Accounts Request");
            try
            {
                var field = ParameterValidation.Validate(externalField, "externalField");
                var body = await BodyValidation.ValidateAsync(req.Body);
                var accountsResponse = await _accountApiService.UpsertAccountsAsync(body, field);
                var response = await ResponseUtility.FromApiResponse(req, accountsResponse);
                _logger.LogInformation("AccountFunctions: Put Salesforce Accounts Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }
    }
}
