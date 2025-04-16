namespace Tests.Fixtures
{
    public interface IAccountFunctions
    {
        public Task<HttpResponseData> GetAccounts([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts")] HttpRequestData req);
        public Task<HttpResponseData> GetAccountById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/{accountId}")] HttpRequestData req, string accountId);
        public Task<HttpResponseData> GetAccountsByFilter([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/filter/{where}")] HttpRequestData req, string where, bool? isAnd);
        public Task<HttpResponseData> PostAccounts([HttpTrigger(AuthorizationLevel.Function, "post", Route = "accounts")] HttpRequestData req);
        public Task<HttpResponseData> PatchAccounts([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "accounts")] HttpRequestData req);
        public Task<HttpResponseData> PutAccounts([HttpTrigger(AuthorizationLevel.Function, "put", Route = "accounts/external/{externalField}")] HttpRequestData req, string externalField);
    }

    public class AccountFunctionsMockedFixture : IDisposable
    {
        public Mock<IAccountFunctions> AccountFunctions { get; }

        public AccountFunctionsMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            AccountFunctions = new Mock<IAccountFunctions>();
        }

        public void Dispose()
        {
        }
    }
}