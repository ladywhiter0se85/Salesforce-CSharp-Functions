namespace Tests.Fixtures
{
    public interface IApiService
    {
        public Task<string> GetAccessTokenAsync();
        public Task<string> GetRequestAsync(string accessToken, string sObjectName, string query);
        public Task<string> PostRequesAsynct(string accessToken, string sObjectName, string data);
        Task<string> PatchRequestAsync(string accessToken, string sObjectName, string data);
        Task<string> UpsertRequestAsync(string accessToken, string sObjectName, string data, string externalField);
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