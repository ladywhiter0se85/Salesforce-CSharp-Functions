namespace Tests.Services.Live
{
    public class AccountApiServiceLiveTest(AccountApiServiceLiveFixture accountApiServiceFixture) : IClassFixture<AccountApiServiceLiveFixture>
    {
        public AccountApiService _accountApiService = accountApiServiceFixture.AccountApiService;

        [Fact]
        public async Task GetAllAccountsAsyncTest()
        {
            Initialize();
            var resp = await _accountApiService.GetAllAccountsAsync();
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountByIdAsyncTest()
        {
            Initialize();
            var resp = await _accountApiService.GetAccountByIdAsync("001gK000002OgxyQAC");
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountsByFilterAsyncOneToOneAndTest()
        {
            Initialize();
            var filter = "BillingState:CA&BillingCountryCode:US";
            var resp = await _accountApiService.GetAccountsByFilterAsync(filter, true);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountsByFilterAsyncOneToManyTest()
        {
            Initialize();
            var filter = "BillingState:CA,British Columbia";
            var resp = await _accountApiService.GetAccountsByFilterAsync(filter, true);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountsByFilterAsyncManyToOneOrTest()
        {
            Initialize();
            var filter = "Name:Pyramid Construction Inc.,University of Arizona&Id:001gK000002OgxzQAC";
            var resp = await _accountApiService.GetAccountsByFilterAsync(filter, false);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PostAccountsAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Account/postAccountsRequest.json");
            var resp = await _accountApiService.PostAccountsAsync(body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PatchAccountsAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Account/patchAccountsRequest.json");
            var resp = await _accountApiService.PatchAccountsAsync(body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task UpsertAccountsAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Account/upsertAccountsRequest.json");
            var externalField = "Id";
            var resp = await _accountApiService.UpsertAccountsAsync(body, externalField);
            Assert.NotNull(resp);
        }

        private void Initialize()
        {
            //Verify AccountApiService
            Assert.NotNull(_accountApiService);
        }
    }
}