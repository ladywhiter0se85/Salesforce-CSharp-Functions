using Microsoft.Extensions.Logging;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Queries;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Services
{
    public class OpportunityApiService
    {
        private ApiService _apiService;
        private ILogger _logger;
        private readonly string _sObjectName = "Opportunity";

        public OpportunityApiService(ILogger logger)
        {
            _apiService = new ApiService(logger);
            _logger = logger;
        }

        public async Task<ApiResponse<List<Opportunity>>> GetAllOpportunitiesAsync()
        {
            return await ServiceUtility.GetSObjectResponseAsync<List<Opportunity>, List<Opportunity>>(_apiService, _logger, _sObjectName, OpportunityQueries.GetAllOpportunitiesQuery, convertResponse: result => result);
        }

        public async Task<ApiResponse<Opportunity>> GetOpportunityByIdAsync(string opportunityId)
        {
            return await ServiceUtility.GetSObjectResponseAsync<Opportunity, List<Opportunity>>(_apiService, _logger, _sObjectName, OpportunityQueries.GetOpportunityByIdQuery + $"'{opportunityId}'", convertResponse: result => result.First());
        }

        public async Task<ApiResponse<List<Opportunity>>> GetOpportunitiesByFilterAsync(string filter, bool isAnd)
        {
            return await ServiceUtility.GetSObjectResponseAsync<List<Opportunity>, List<Opportunity>>(_apiService, _logger, _sObjectName, OpportunityQueries.GetOpportunitiesByFilterQuery + ServiceUtility.BuildWhereFilter(filter, isAnd), convertResponse: result => result);
        }

        public async Task<ApiResponse<List<OperationResponse>>> PostOpportunitiesAsync(string body)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Post, body);
        }

        public async Task<ApiResponse<List<OperationResponse>>> PatchOpportunitiesAsync(string body)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Patch, body);
        }

        public async Task<ApiResponse<List<OperationResponse>>> UpsertOpportunitiesAsync(string body, string externalField)
        {
            return await ServiceUtility.ProcessSObjectOperationAsync(_apiService, _logger, _sObjectName, HttpMethod.Put, body, externalField);
        }
    }
}