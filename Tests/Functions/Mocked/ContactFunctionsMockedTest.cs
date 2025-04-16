namespace Tests.Functions.Mocked
{
    public class ContactFuctionsTest(ContactFunctionsMockedFixture contactFunctionsFixture) : IClassFixture<ContactFunctionsMockedFixture>
    {
        public Mock<IContactFunctions> _contactFunctions = contactFunctionsFixture.ContactFunctions;

        [Fact]
        public async Task GetContactsTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, contacts);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.GetContacts(request)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.GetContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Contact>>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Paris", resp[1].MailingAddress!.City);
        }

        [Fact]
        public async Task GetContactByIdTest()
        {
            var contactId = "mockedContactOneId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, contacts![0]);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.GetContactById(request, contactId)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.GetContactById(request, contactId);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Contact>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedContactOne@email.com", resp.Email);
        }

        [Fact]
        public async Task GetContactByIdNoContactIdTest()
        {
            var msg = "The parameter 'contactId' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.GetContactById(request, "")).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.GetContactById(request, "");
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task GetContactsByFilterTest()
        {
            var filter = "Id:mockedContactIdOne";
            var isAnd = true;
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, contacts);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.GetContactsByFilter(request, filter, isAnd)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.GetContactsByFilter(request, filter, isAnd);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Contact>>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("525 S. Lexington Ave", resp[0].MailingAddress!.Street);
        }

        [Fact]
        public async Task GetContactsByFilterNoFilterTest()
        {
            var isAnd = true;
            var msg = "The parameter 'where' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.GetContactsByFilter(request, "", isAnd)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.GetContactsByFilter(request, "", isAnd);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task GetContactsByFilterNoIsAndTest()
        {
            var filter = "Id:mockedContactOneId";
            var msg = "The parameter 'isAnd' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.GetContactsByFilter(request, filter, null)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.GetContactsByFilter(request, filter, null);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PostContactsTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/postContactsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _contactFunctions.Setup(x => x.PostContacts(request)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PostContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedSObjectId", resp[0].Id);
        }

        [Fact]
        public async Task PostContactsNoBodyTest()
        {
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.PostContacts(request)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PostContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PatchContactsTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/patchContactsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _contactFunctions.Setup(x => x.PatchContacts(request)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PatchContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedSObjectId", resp[0].Id);
        }

        [Fact]
        public async Task PatchContactsNoBodyTest()
        {
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.PatchContacts(request)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PatchContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PatchContactsNoIdInBodyTest()
        {
            var msg = "Each object in a PATCH request must include an 'Id' field.";
            var body = "[{\"attributes\":{\"type\":\"Contact\"}, \"Description\":\"CH Test Contact Update\"}]";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _contactFunctions.Setup(x => x.PatchContacts(request)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PatchContacts(request);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PutContactsTest()
        {
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/upsertContactsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _contactFunctions.Setup(x => x.PutContacts(request, externalField)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PutContacts(request, externalField);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.OK, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedSObjectId", resp[0].Id);
        }

        [Fact]
        public async Task PutContactsNoExternalFieldTest()
        {
            var msg = "The parameter 'externalField' is required.";
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/upsertContactsRequest.json");
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _contactFunctions.Setup(x => x.PutContacts(request, "")).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PutContacts(request, "");
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PutContactsNoBodyTest()
        {
            var externalField = "Id";
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _contactFunctions.Setup(x => x.PutContacts(request, externalField)).ReturnsAsync(response);

            var contactsResponse = await _contactFunctions.Object.PutContacts(request, externalField);
            Assert.NotNull(contactsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, contactsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(contactsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }
    }
}