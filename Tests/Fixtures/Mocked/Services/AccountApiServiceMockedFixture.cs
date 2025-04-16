namespace Tests.Fixtures
{
    public interface IAccountApiService
    {
        public Task<ApiResponse<List<Account>>> GetAllAccountsAsync();
        public Task<ApiResponse<Account>> GetAccountByIdAsync(string accountId);
        public Task<ApiResponse<List<Account>>> GetAccountsByFilterAsync(string filter, bool isAnd);
        public Task<ApiResponse<List<OperationResponse>>> PostAccountsAsync(string body);
        public Task<ApiResponse<List<OperationResponse>>> PatchAccountsAsync(string body);
        public Task<ApiResponse<List<OperationResponse>>> UpsertAccountsAsync(string body, string externalField);
    }

    public class AccountApiServiceMockedFixture : IDisposable
    {
        public Mock<IAccountApiService> AccountApiService { get; }

        public AccountApiServiceMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            AccountApiService = new Mock<IAccountApiService>();
        }

        public void Dispose()
        {
        }
    }
}