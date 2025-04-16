namespace Tests.Fixtures
{
    public interface IContactFunctions
    {
        public Task<HttpResponseData> GetContacts([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts")] HttpRequestData req);
        public Task<HttpResponseData> GetContactById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts/{contactId}")] HttpRequestData req, string contactId);
        public Task<HttpResponseData> GetContactsByFilter([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts/filter/{where}")] HttpRequestData req, string where, bool? isAnd);
        public Task<HttpResponseData> PostContacts([HttpTrigger(AuthorizationLevel.Function, "post", Route = "contacts")] HttpRequestData req);
        public Task<HttpResponseData> PatchContacts([HttpTrigger(AuthorizationLevel.Function, "patch", Route = "contacts")] HttpRequestData req);
        public Task<HttpResponseData> PutContacts([HttpTrigger(AuthorizationLevel.Function, "put", Route = "contacts/external/{externalField}")] HttpRequestData req, string externalField);
    }

    public class ContactFunctionsMockedFixture : IDisposable
    {
        public Mock<IContactFunctions> ContactFunctions { get; }

        public ContactFunctionsMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            ContactFunctions = new Mock<IContactFunctions>();
        }

        public void Dispose()
        {
        }
    }
}