namespace Tests.Functions.Live
{
    public class ContactFuctionsLiveTest(ContactFunctionsLiveFixture contactFunctionsFixture) : IClassFixture<ContactFunctionsLiveFixture>
    {
        public ContactFunctions _contactFunctions = contactFunctionsFixture.ContactFunctions;

        [Fact]
        public async Task GetContactsTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.GetContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Contact>>(contactsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactByIdTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.GetContactById(request, "003gK000000SYQ9QAO");
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Contact>(contactsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactByIdNoContactIdTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.GetContactById(request, "");
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'contactId' is required.", resp);
        }

        [Fact]
        public async Task GetContactsByFilterTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.GetContactsByFilter(request, "MailingCountryCode:US&MailingState:KS", true);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Contact>>(contactsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactsByFilterNoFilterTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.GetContactsByFilter(request, "", true);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'where' is required.", resp);
        }

        [Fact]
        public async Task GetContactsByFilterNoIsAndTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.GetContactsByFilter(request, "BillingState:CA,British Columbia", null);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'isAnd' is required.", resp);
        }

        [Fact]
        public async Task PostContactsTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/postContactsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var contactsResponse = await _contactFunctions.PostContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(contactsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PostContactsNoBodyTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.PostContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        [Fact]
        public async Task PatchContactsTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/patchContactsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var contactsResponse = await _contactFunctions.PatchContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(contactsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PatchContactsNoBodyTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.PatchContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        [Fact]
        public async Task PatchContactsNoIdInBodyTest()
        {
            Initialize();
            var body = "[{\"attributes\":{\"type\":\"Contact\"}, \"Description\":\"CH Test Contact Update\"}]";

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var contactsResponse = await _contactFunctions.PatchContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Each object in a PATCH request must include an 'Id' field.", resp);
        }

        [Fact]
        public async Task PutContactsTest()
        {
            Initialize();
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/upsertContactsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var contactsResponse = await _contactFunctions.PutContacts(request, externalField);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(contactsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PutContactsNoExternalFieldTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/upsertContactsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var contactsResponse = await _contactFunctions.PutContacts(request, "");
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'externalField' is required.", resp);
        }

        [Fact]
        public async Task PutContactsNoBodyTest()
        {
            Initialize();
            var externalField = "Id";

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var contactsResponse = await _contactFunctions.PutContacts(request, externalField);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        private void Initialize()
        {
            //Verify ContactFunctions
            Assert.NotNull(_contactFunctions);
        }
    }
}