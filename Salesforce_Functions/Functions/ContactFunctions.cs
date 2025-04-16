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
    public class ContactFunctions
    {
        private readonly ILogger<ContactFunctions> _logger;
        private readonly ContactApiService _contactApiService;

        public ContactFunctions(ILogger<ContactFunctions> logger)
        {
            _logger = logger;
            _contactApiService = new ContactApiService(logger);
        }

        [Function("GetContacts")]
        [OpenApiOperation(operationId: "GetContacts", Summary = "Retrieve a list of Salesforce Contacts", Description = "Retrieves all available Salesforce contacts with basic details.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Contact>), Description = "Successful response with list of Salesforce Contacts", Example = typeof(ContactsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetContacts([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts")] HttpRequestData req)
        {
            _logger.LogInformation("ContactFunctions: Get All Salesforce Contacts Request");
            var contactsResponse = await _contactApiService.GetAllContactsAsync();
            var response = await ResponseUtility.FromApiResponse(req, contactsResponse);
            _logger.LogInformation("ContactFunctions: Get All Salesforce Contacts Request Complete");
            return response;
        }

        [Function("GetContactById")]
        [OpenApiOperation(operationId: "GetContactById", Summary = "Retrieve a Salesforce Contact by Id", Description = "Retrieves a Salesforce contact by id with basic details.")]
        [OpenApiParameter(name: "contactId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Salesforce Contact Id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Contact), Description = "Successful response with a Salesforce Contact", Example = typeof(ContactOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetContactById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts/id/{contactId}")] HttpRequestData req, string contactId)
        {
            _logger.LogInformation("ContactFunctions: Get Salesforce Contact By Id Request");
            try
            {
                var id = ParameterValidation.Validate(contactId, "contactId");
                var contactsResponse = await _contactApiService.GetContactByIdAsync(id);
                var response = await ResponseUtility.FromApiResponse(req, contactsResponse);
                _logger.LogInformation("ContactFunctions: Get Salesforce Contact By Id Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("GetContactsByFilter")]
        [OpenApiOperation(operationId: "GetContactsByFilter", Summary = "Retrieve a Salesforce Contacts by Filter", Description = "Retrieves Salesforce contacts by filter with basic details.")]
        [OpenApiParameter(name: "where", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Where Condition Filters")]
        [OpenApiParameter(name: "isAnd", In = ParameterLocation.Query, Required = true, Type = typeof(bool), Description = "Type of Condition Check AND/OR")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Contact>), Description = "Successful response with list of Salesforce Contacts", Example = typeof(ContactsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> GetContactsByFilter([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts/filter/{where}")] HttpRequestData req, string where, bool? isAnd)
        {
            _logger.LogInformation("ContactFunctions: Get Salesforce Contacts By Filter Request");
            try
            {
                var filter = ParameterValidation.Validate(where, "where");
                var cond = ParameterValidation.Validate(isAnd, "isAnd");
                var contactsResponse = await _contactApiService.GetContactsByFilterAsync(filter, isAnd ?? false);
                var response = await ResponseUtility.FromApiResponse(req, contactsResponse);
                _logger.LogInformation("ContactFunctions: Get Salesforce Contacts By Filter Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PostContacts")]
        [OpenApiOperation(operationId: "PostContacts", Summary = "Create Salesforce Contacts", Description = "Creates Salesforce contacts.")]
        [OpenApiRequestBody("application/json", typeof(List<Contact>), Required = true, Description = "List of Contact objects to create", Example = typeof(ContactsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PostContacts([HttpTrigger(AuthorizationLevel.Function, "post", Route = "contacts")] HttpRequestData req)
        {
            _logger.LogInformation("ContactFunctions: Post Salesforce Contacts Request");
            try
            {
                var body = await BodyValidation.ValidateAsync(req.Body);
                var contactsResponse = await _contactApiService.PostContactsAsync(body);
                var response = await ResponseUtility.FromApiResponse(req, contactsResponse);
                _logger.LogInformation("ContactFunctions: Post Salesforce Contacts Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PatchContacts")]
        [OpenApiOperation(operationId: "PatchContacts", Summary = "Updates Salesforce Contacts", Description = "Updates Salesforce contacts.")]
        [OpenApiRequestBody("application/json", typeof(List<Contact>), Required = true, Description = "List of Contact objects to update", Example = typeof(ContactsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PatchContacts([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "contacts")] HttpRequestData req)
        {
            _logger.LogInformation("ContactFunctions: Patch Salesforce Contacts Request");
            try
            {
                var body = await BodyValidation.ValidateAndCheckIdsAsync(req.Body);
                var contactsResponse = await _contactApiService.PatchContactsAsync(body);
                var response = await ResponseUtility.FromApiResponse(req, contactsResponse);
                _logger.LogInformation("ContactFunctions: Patch Salesforce Contacts Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }

        [Function("PutContacts")]
        [OpenApiOperation(operationId: "PutContacts", Summary = "Upsert Salesforce Contacts", Description = "Upsert Salesforce contacts.")]
        [OpenApiParameter(name: "externalField", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Salesforce External field used for upsert.")]
        [OpenApiRequestBody("application/json", typeof(List<Contact>), Required = true, Description = "List of Contact objects to upsert", Example = typeof(ContactsOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OperationResponse>), Description = "Successful response with Operation Responses", Example = typeof(OperationResponseOpenApiExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid input or request format.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(string), Description = "An unexpected error occurred.")]
        public async Task<HttpResponseData> PutContacts([HttpTrigger(AuthorizationLevel.Function, "put", Route = "contacts/external/{externalField}")] HttpRequestData req, string externalField)
        {
            _logger.LogInformation("ContactFunctions: Put Salesforce Contacts Request");
            try
            {
                var field = ParameterValidation.Validate(externalField, "externalField");
                var body = await BodyValidation.ValidateAsync(req.Body);
                var contactsResponse = await _contactApiService.UpsertContactsAsync(body, field);
                var response = await ResponseUtility.FromApiResponse(req, contactsResponse);
                _logger.LogInformation("ContactFunctions: Put Salesforce Contacts Request Complete");
                return response;
            }
            catch (ApiResponseException<string> e)
            {
                return await ResponseUtility.FromApiResponse(req, e.ApiResponse);
            }
        }
    }
}
