namespace Tests.Services.Mocked
{
    public class ContactApiServiceMockedTest(ContactApiServiceMockedFixture contactApiServiceFixture) : IClassFixture<ContactApiServiceMockedFixture>
    {
        public Mock<IContactApiService> _contactApiService = contactApiServiceFixture.ContactApiService;

        [Fact]
        public async Task GetAllContactsAsyncTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);

            _contactApiService.Setup(x => x.GetAllContactsAsync()).ReturnsAsync(new ApiResponse<List<Contact>>(contacts));

            var resp = await _contactApiService.Object.GetAllContactsAsync();
            Assert.NotNull(resp);
            Assert.Equal("Mocked Contact One", resp.Data![0].Name);
        }

        [Fact]
        public async Task GetContactByIdAsyncTest()
        {
            var contactId = "mockedContactIdOne";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactResponse.json");
            var contact = JsonConvert.DeserializeObject<Contact>(json)!;
            Assert.NotNull(contact);

            _contactApiService.Setup(x => x.GetContactByIdAsync(contactId)).ReturnsAsync(new ApiResponse<Contact>(contact));

            ApiResponse<Contact> resp = await _contactApiService.Object.GetContactByIdAsync(contactId)!;
            Assert.NotNull(resp);
            Assert.NotNull(resp.Data);
            Assert.Equal("27215", resp.Data!.MailingAddress!.PostalCode);
        }

        [Fact]
        public async Task GetContactsByFilterAsyncOneToOneAndTest()
        {
            var filter = "MailingCountryCode:FR&MailingCity:Paris";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);

            _contactApiService.Setup(x => x.GetContactsByFilterAsync(filter, true)).ReturnsAsync(new ApiResponse<List<Contact>>(contacts));

            var resp = await _contactApiService.Object.GetContactsByFilterAsync(filter, true);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task GetContactsByFilterAsyncOneToManyTest()
        {
            var filter = "MailingCountry:USA,France";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);

            _contactApiService.Setup(x => x.GetContactsByFilterAsync(filter, true)).ReturnsAsync(new ApiResponse<List<Contact>>(contacts));

            var resp = await _contactApiService.Object.GetContactsByFilterAsync(filter, true);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task GetContactsByFilterAsyncManyToOneOrTest()
        {
            var filter = "AccountId:mockedAccountIdOne,mockedAccountIdTwo&Id:mockedContactIdOne";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Contact/contactsResponse.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);

            _contactApiService.Setup(x => x.GetContactsByFilterAsync(filter, false)).ReturnsAsync(new ApiResponse<List<Contact>>(contacts));

            var resp = await _contactApiService.Object.GetContactsByFilterAsync(filter, false);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task PostContactsAsyncTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/postContactsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);

            _contactApiService.Setup(x => x.PostContactsAsync(body!)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _contactApiService.Object.PostContactsAsync(body!);
            Assert.NotNull(resp);
            Assert.Single(resp.Data!);
        }

        [Fact]
        public async Task PatchContactsAsyncTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/patchContactsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);

            _contactApiService.Setup(x => x.PatchContactsAsync(body!)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _contactApiService.Object.PatchContactsAsync(body!);
            Assert.NotNull(resp);
            Assert.Single(resp.Data!);
        }

        [Fact]
        public async Task UpsertContactsAsyncTest()
        {
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/upsertContactsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            operationResp!.Add(new OperationResponse() { Id = "mockedContactIdTwo" });

            _contactApiService.Setup(x => x.UpsertContactsAsync(body!, externalField)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _contactApiService.Object.UpsertContactsAsync(body!, externalField);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }
    }
}