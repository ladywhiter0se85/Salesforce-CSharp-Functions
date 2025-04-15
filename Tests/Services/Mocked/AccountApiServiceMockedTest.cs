namespace Tests.Services.Mocked
{
    public class AccountApiServiceMockedTest(AccountApiServiceMockedFixture accountApiServiceFixture) : IClassFixture<AccountApiServiceMockedFixture>
    {
        public Mock<IAccountApiService> _accountApiService = accountApiServiceFixture.AccountApiService;

        [Fact]
        public async Task GetAllAccountsAsyncTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);

            _accountApiService.Setup(x => x.GetAllAccountsAsync()).ReturnsAsync(new ApiResponse<List<Account>>(accounts));

            var resp = await _accountApiService.Object.GetAllAccountsAsync();
            Assert.NotNull(resp);
            Assert.Equal("Mocked Account One", resp.Data![0].Name);
        }

        [Fact]
        public async Task GetAccountByIdAsyncTest()
        {
            var accountId = "mockedAccountTwoId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountResponse.json");
            var account = JsonConvert.DeserializeObject<Account>(json)!;
            Assert.NotNull(account);

            _accountApiService.Setup(x => x.GetAccountByIdAsync(accountId)).ReturnsAsync(new ApiResponse<Account>(account));

            ApiResponse<Account> resp = await _accountApiService.Object.GetAccountByIdAsync(accountId)!;
            Assert.NotNull(resp);
            Assert.NotNull(resp.Data);
            Assert.Equal("Mocked Account Two", resp.Data!.Name);
        }

        [Fact]
        public async Task GetAccountsByFilterAsyncOneToOneAndTest()
        {
            var filter = "Type:Customer - Direct&BillingCountryCode:US";
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);

            _accountApiService.Setup(x => x.GetAccountsByFilterAsync(filter, true)).ReturnsAsync(new ApiResponse<List<Account>>(accounts));

            var resp = await _accountApiService.Object.GetAccountsByFilterAsync(filter, true);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task GetAccountsByFilterAsyncOneToManyTest()
        {
            var filter = "BillingState:TX,NC";
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);

            _accountApiService.Setup(x => x.GetAccountsByFilterAsync(filter, true)).ReturnsAsync(new ApiResponse<List<Account>>(accounts));

            var resp = await _accountApiService.Object.GetAccountsByFilterAsync(filter, true);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task GetAccountsByFilterAsyncManyToOneOrTest()
        {
            var filter = "Name:Mocked Account One,Mocked Account Two&Id:mockedAccountTwoId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);

            _accountApiService.Setup(x => x.GetAccountsByFilterAsync(filter, false)).ReturnsAsync(new ApiResponse<List<Account>>(accounts));

            var resp = await _accountApiService.Object.GetAccountsByFilterAsync(filter, false);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task PostAccountsAsyncTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/postAccountsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);

            _accountApiService.Setup(x => x.PostAccountsAsync(body!)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _accountApiService.Object.PostAccountsAsync(body!);
            Assert.NotNull(resp);
            Assert.Single(resp.Data!);
        }

        [Fact]
        public async Task PatchAccountsAsyncTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/patchAccountsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);

            _accountApiService.Setup(x => x.PatchAccountsAsync(body!)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _accountApiService.Object.PatchAccountsAsync(body!);
            Assert.NotNull(resp);
            Assert.Single(resp.Data!);
        }

        [Fact]
        public async Task UpsertAccountsAsyncTest()
        {
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/upsertAccountsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            operationResp!.Add(new OperationResponse() { Id = "mockedAccountIdTwo" });

            _accountApiService.Setup(x => x.UpsertAccountsAsync(body!, externalField)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _accountApiService.Object.UpsertAccountsAsync(body!, externalField);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }
    }
}