using Microsoft.Extensions.Logging;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Queries;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Services
{
    public class AccountApiService
    {
        private ApiService _apiService;
        private ILogger _logger;
        private readonly string _sObjectName = "Account";

        public AccountApiService(ILogger logger)
        {
            _apiService = new ApiService(logger);
            _logger = logger;
        }

        public async Task<ApiResponse<List<Account>>> GetAllAccountsAsync()
        {
            return await ServiceUtility.GetSObjectResponseAsync<List<Account>, List<Account>>(_apiService, _logger, _sObjectName, AccountQueries.GetAllAccountsQuery, convertResponse: result => result);
        }

        public async Task<ApiResponse<Account>> GetAccountByIdAsync(string accountId)
        {
            return await ServiceUtility.GetSObjectResponseAsync<Account, List<Account>>(_apiService, _logger, _sObjectName, AccountQueries.GetAccountByIdQuery + $"'{accountId}'", convertResponse: result => result.First());
        }

        public async Task<ApiResponse<List<Account>>> GetAccountsByFilterAsync(string filter, bool isAnd)
        {
            return await ServiceUtility.GetSObjectResponseAsync<List<Account>, List<Account>>(_apiService, _logger, _sObjectName, AccountQueries.GetAccountByFilterQuery + ServiceUtility.BuildWhereFilter(filter, isAnd), convertResponse: result => result);
        }

        public async Task<ApiResponse<List<OperationResponse>>> PostAccountsAsync(string body)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Post, body);
        }

        public async Task<ApiResponse<List<OperationResponse>>> PatchAccountsAsync(string body)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Patch, body);
        }

        public async Task<ApiResponse<List<OperationResponse>>> UpsertAccountsAsync(string body, string externalField)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Put, body, externalField);
        }
    }
}