namespace Tests.Functions.Live
{
    public class AccountFuctionsLiveTest(AccountFunctionsLiveFixture accountFunctionsFixture) : IClassFixture<AccountFunctionsLiveFixture>
    {
        public AccountFunctions _accountFunctions = accountFunctionsFixture.AccountFunctions;

        [Fact]
        public async Task GetAccountsTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.GetAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Account>>(accountsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountByIdTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.GetAccountById(request, "001gK000002OgxzQAC");
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Account>(accountsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountByIdNoAccountIdTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.GetAccountById(request, "");
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'accountId' is required.", resp);
        }

        [Fact]
        public async Task GetAccountsByFilterTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.GetAccountsByFilter(request, "BillingState:CA,British Columbia", true);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Account>>(accountsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetAccountsByFilterNoFilterTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.GetAccountsByFilter(request, "", true);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'where' is required.", resp);
        }

        [Fact]
        public async Task GetAccountsByFilterNoIsAndTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.GetAccountsByFilter(request, "BillingState:CA,British Columbia", null);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'isAnd' is required.", resp);
        }

        [Fact]
        public async Task PostAccountsTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Account/postAccountsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var accountsResponse = await _accountFunctions.PostAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(accountsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PostAccountsNoBodyTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.PostAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        [Fact]
        public async Task PatchAccountsTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Account/patchAccountsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var accountsResponse = await _accountFunctions.PatchAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(accountsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PatchAccountsNoBodyTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.PatchAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        [Fact]
        public async Task PatchAccountsNoIdInBodyTest()
        {
            Initialize();
            var body = "[{\"attributes\":{\"type\":\"Account\"}, \"Description\":\"CH Test Account Update\"}]";

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var accountsResponse = await _accountFunctions.PatchAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Each object in a PATCH request must include an 'Id' field.", resp);
        }

        [Fact]
        public async Task PutAccountsTest()
        {
            Initialize();
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/Account/upsertAccountsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var accountsResponse = await _accountFunctions.PutAccounts(request, externalField);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(accountsResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PutAccountsNoExternalFieldTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Account/upsertAccountsRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var accountsResponse = await _accountFunctions.PutAccounts(request, "");
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'externalField' is required.", resp);
        }

        [Fact]
        public async Task PutAccountsNoBodyTest()
        {
            Initialize();
            var externalField = "Id";

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var accountsResponse = await _accountFunctions.PutAccounts(request, externalField);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        private void Initialize()
        {
            //Verify AccountFunctions
            Assert.NotNull(_accountFunctions);
        }
    }
}