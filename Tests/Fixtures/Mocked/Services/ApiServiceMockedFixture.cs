namespace Tests.Fixtures
{
    public interface IApiService
    {
        public Task<string> GetAccessTokenAsync();
        public Task<string> GetQueryAsync(string accessToken, string sObjectName, string query);
        public Task<string> GetDescribeAsync(string accessToken, string sObjectName);
        public Task<string> PostRequestAsync(string accessToken, string sObjectName, string data);
        public Task<string> PatchRequestAsync(string accessToken, string sObjectName, string data);
        public Task<string> UpsertRequestAsync(string accessToken, string sObjectName, string data, string externalField);
    }

    public class ApiServiceMockedFixture : IDisposable
    {
        public Mock<IApiService> ApiService { get; }

        public ApiServiceMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;
            ApiService = new Mock<IApiService>();
        }

        public void Dispose()
        {
        }
    }
}