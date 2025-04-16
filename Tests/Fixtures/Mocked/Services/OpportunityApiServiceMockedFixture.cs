namespace Tests.Fixtures
{
    public interface IOpportunityApiService
    {
        public Task<ApiResponse<List<Opportunity>>> GetAllOpportunitiesAsync();
        public Task<ApiResponse<Opportunity>> GetOpportunityByIdAsync(string accountId);
        public Task<ApiResponse<List<Opportunity>>> GetOpportunitiesByFilterAsync(string filter, bool isAnd);
        public Task<ApiResponse<List<OperationResponse>>> PostOpportunitiesAsync(string body);
        public Task<ApiResponse<List<OperationResponse>>> PatchOpportunitiesAsync(string body);
        public Task<ApiResponse<List<OperationResponse>>> UpsertOpportunitiesAsync(string body, string externalField);
    }

    public class OpportunityApiServiceMockedFixture : IDisposable
    {
        public Mock<IOpportunityApiService> OpportunityApiService { get; }

        public OpportunityApiServiceMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            OpportunityApiService = new Mock<IOpportunityApiService>();
        }

        public void Dispose()
        {
        }
    }
}