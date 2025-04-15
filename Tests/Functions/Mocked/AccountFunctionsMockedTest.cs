namespace Tests.Functions.Mocked
{
    public class AccountFuctionsTest(AccountFunctionsMockedFixture accountFunctionsFixture) : IClassFixture<AccountFunctionsMockedFixture>
    {
        public Mock<IAccountFunctions> _accountFunctions = accountFunctionsFixture.AccountFunctions;

        [Fact]
        public async Task GetAccountsTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, accounts);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.GetAccounts(request)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.GetAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Account>>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Burlington", resp[1].BillingAddress!.City);
        }

        [Fact]
        public async Task GetAccountByIdTest()
        {
            var accountId = "mockedAccountOneId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, accounts![0]);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.GetAccountById(request, accountId)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.GetAccountById(request, accountId);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Account>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Austin", resp.BillingAddress!.City);
        }

        [Fact]
        public async Task GetAccountByIdNoAccountIdTest()
        {
            var msg = "The parameter 'accountId' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.GetAccountById(request, "")).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.GetAccountById(request, "");
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task GetAccountsByFilterTest()
        {
            var filter = "Id:mockedAccountOneId";
            var isAnd = true;
            var json = ResponseUtility.ReadFile("Resources/Mocked/accountsResponse.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, accounts);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.GetAccountsByFilter(request, filter, isAnd)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.GetAccountsByFilter(request, filter, isAnd);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Account>>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Austin", resp[0].BillingAddress!.City);
        }

        [Fact]
        public async Task GetAccountsByFilterNoFilterTest()
        {
            var isAnd = true;
            var msg = "The parameter 'where' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.GetAccountsByFilter(request, "", isAnd)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.GetAccountsByFilter(request, "", isAnd);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task GetAccountsByFilterNoIsAndTest()
        {
            var filter = "Id:mockedAccountOneId";
            var msg = "The parameter 'isAnd' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.GetAccountsByFilter(request, filter, null)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.GetAccountsByFilter(request, filter, null);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PostAccountsTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/postAccountsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _accountFunctions.Setup(x => x.PostAccounts(request)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PostAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedAccountId", resp[0].Id);
        }

        [Fact]
        public async Task PostAccountsNoBodyTest()
        {
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.PostAccounts(request)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PostAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PatchAccountsTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/patchAccountsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _accountFunctions.Setup(x => x.PatchAccounts(request)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PatchAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedAccountId", resp[0].Id);
        }

        [Fact]
        public async Task PatchAccountsNoBodyTest()
        {
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.PatchAccounts(request)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PatchAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PatchAccountsNoIdInBodyTest()
        {
            var msg = "Each object in a PATCH request must include an 'Id' field.";
            var body = "[{\"attributes\":{\"type\":\"Account\"}, \"Description\":\"CH Test Account Update\"}]";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _accountFunctions.Setup(x => x.PatchAccounts(request)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PatchAccounts(request);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PutAccountsTest()
        {
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/upsertAccountsRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _accountFunctions.Setup(x => x.PutAccounts(request, externalField)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PutAccounts(request, externalField);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.OK, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedAccountId", resp[0].Id);
        }

        [Fact]
        public async Task PutAccountsNoExternalFieldTest()
        {
            var msg = "The parameter 'externalField' is required.";
            var body = ResponseUtility.ReadFile("Resources/Live/upsertAccountsRequest.json");
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _accountFunctions.Setup(x => x.PutAccounts(request, "")).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PutAccounts(request, "");
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PutAccountsNoBodyTest()
        {
            var externalField = "Id";
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _accountFunctions.Setup(x => x.PutAccounts(request, externalField)).ReturnsAsync(response);

            var accountsResponse = await _accountFunctions.Object.PutAccounts(request, externalField);
            Assert.NotNull(accountsResponse);
            Assert.Equal(HttpStatusCode.BadRequest, accountsResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(accountsResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }
    }
}