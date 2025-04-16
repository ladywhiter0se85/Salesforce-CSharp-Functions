using Microsoft.Extensions.Logging;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Queries;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Services
{
    public class ContactApiService
    {
        private ApiService _apiService;
        private ILogger _logger;
        private readonly string _sObjectName = "Contact";

        public ContactApiService(ILogger logger)
        {
            _apiService = new ApiService(logger);
            _logger = logger;
        }

        public async Task<ApiResponse<List<Contact>>> GetAllContactsAsync()
        {
            return await ServiceUtility.GetSObjectResponseAsync<List<Contact>, List<Contact>>(_apiService, _logger, _sObjectName, ContactQueries.GetAllContactsQuery, convertResponse: result => result);
        }

        public async Task<ApiResponse<Contact>> GetContactByIdAsync(string contactId)
        {
            return await ServiceUtility.GetSObjectResponseAsync<Contact, List<Contact>>(_apiService, _logger, _sObjectName, ContactQueries.GetContactByIdQuery + $"'{contactId}'", convertResponse: result => result.First());
        }

        public async Task<ApiResponse<List<Contact>>> GetContactsByFilterAsync(string filter, bool isAnd)
        {
            return await ServiceUtility.GetSObjectResponseAsync<List<Contact>, List<Contact>>(_apiService, _logger, _sObjectName, ContactQueries.GetContactsByFilterQuery + ServiceUtility.BuildWhereFilter(filter, isAnd), convertResponse: result => result);
        }

        public async Task<ApiResponse<List<OperationResponse>>> PostContactsAsync(string body)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Post, body);
        }

        public async Task<ApiResponse<List<OperationResponse>>> PatchContactsAsync(string body)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Patch, body);
        }

        public async Task<ApiResponse<List<OperationResponse>>> UpsertContactsAsync(string body, string externalField)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Put, body, externalField);
        }
    }
}