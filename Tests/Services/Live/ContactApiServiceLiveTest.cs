namespace Tests.Services.Live
{
    public class ContactApiServiceLiveTest(ContactApiServiceLiveFixture contactApiServiceFixture) : IClassFixture<ContactApiServiceLiveFixture>
    {
        public ContactApiService _contactApiService = contactApiServiceFixture.ContactApiService;

        [Fact]
        public async Task GetAllContactsAsyncTest()
        {
            Initialize();
            var resp = await _contactApiService.GetAllContactsAsync();
            var json = JsonConvert.SerializeObject(resp);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactByIdAsyncTest()
        {
            Initialize();
            var resp = await _contactApiService.GetContactByIdAsync("003gK000000SYQ9QAO");
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactsByFilterAsyncOneToOneAndTest()
        {
            Initialize();
            var filter = "MailingCountryCode:US&MailingState:KS";
            var resp = await _contactApiService.GetContactsByFilterAsync(filter, true);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactsByFilterAsyncOneToManyTest()
        {
            Initialize();
            var filter = "MailingState:KS,CA";
            var resp = await _contactApiService.GetContactsByFilterAsync(filter, true);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetContactsByFilterAsyncManyToOneOrTest()
        {
            Initialize();
            var filter = "Name:Sean Forbes,Edna Frank&Id:003gK000000SYQFQA4";
            var resp = await _contactApiService.GetContactsByFilterAsync(filter, false);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PostContactsAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/postContactsRequest.json");
            var resp = await _contactApiService.PostContactsAsync(body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PatchContactsAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/patchContactsRequest.json");
            var resp = await _contactApiService.PatchContactsAsync(body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task UpsertContactsAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Contact/upsertContactsRequest.json");
            var externalField = "Id";
            var resp = await _contactApiService.UpsertContactsAsync(body, externalField);
            Assert.NotNull(resp);
        }

        private void Initialize()
        {
            //Verify ContactApiService
            Assert.NotNull(_contactApiService);
        }
    }
}