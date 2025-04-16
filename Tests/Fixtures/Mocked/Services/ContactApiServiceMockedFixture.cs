namespace Tests.Fixtures
{
    public interface IContactApiService
    {
        public Task<ApiResponse<List<Contact>>> GetAllContactsAsync();
        public Task<ApiResponse<Contact>> GetContactByIdAsync(string contactId);
        public Task<ApiResponse<List<Contact>>> GetContactsByFilterAsync(string filter, bool isAnd);
        public Task<ApiResponse<List<OperationResponse>>> PostContactsAsync(string body);
        public Task<ApiResponse<List<OperationResponse>>> PatchContactsAsync(string body);
        public Task<ApiResponse<List<OperationResponse>>> UpsertContactsAsync(string body, string externalField);
    }

    public class ContactApiServiceMockedFixture : IDisposable
    {
        public Mock<IContactApiService> ContactApiService { get; }

        public ContactApiServiceMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            ContactApiService = new Mock<IContactApiService>();
        }

        public void Dispose()
        {
        }
    }
}