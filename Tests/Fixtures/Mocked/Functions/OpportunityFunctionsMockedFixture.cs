namespace Tests.Fixtures
{
    public interface IOpportunityFunctions
    {
        public Task<HttpResponseData> GetOpportunities([HttpTrigger(AuthorizationLevel.Function, "get", Route = "opportunities")] HttpRequestData req);
        public Task<HttpResponseData> GetOpportunityById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "opportunities/{opportunityId}")] HttpRequestData req, string opportunityId);
        public Task<HttpResponseData> GetOpportunitiesByFilter([HttpTrigger(AuthorizationLevel.Function, "get", Route = "opportunities/filter/{where}")] HttpRequestData req, string where, bool? isAnd);
        public Task<HttpResponseData> PostOpportunities([HttpTrigger(AuthorizationLevel.Function, "post", Route = "opportunities")] HttpRequestData req);
        public Task<HttpResponseData> PatchOpportunities([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "opportunities")] HttpRequestData req);
        public Task<HttpResponseData> PutOpportunities([HttpTrigger(AuthorizationLevel.Function, "put", Route = "opportunities/external/{externalField}")] HttpRequestData req, string externalField);
    }

    public class OpportunityFunctionsMockedFixture : IDisposable
    {
        public Mock<IOpportunityFunctions> OpportunityFunctions { get; }

        public OpportunityFunctionsMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            OpportunityFunctions = new Mock<IOpportunityFunctions>();
        }

        public void Dispose()
        {
        }
    }
}