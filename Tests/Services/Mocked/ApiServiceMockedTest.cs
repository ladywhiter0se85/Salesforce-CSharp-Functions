namespace Tests.Services.Mocked;

public class ApiServiceMockedTest(ApiServiceMockedFixture apiServiceFixture) : IClassFixture<ApiServiceMockedFixture>
{
    public Mock<IApiService> _apiService = apiServiceFixture.ApiService;
    private string _accessToken = "mockedAccessToken";
    private string _operationResponse = "[{\"id\":\"mockedAccountId\",\"success\":true,\"errors\":[]}]";

    [Fact]
    public async Task GetAccessTokenAsyncTest()
    {
        _apiService.Setup(x => x.GetAccessTokenAsync()).ReturnsAsync(_accessToken);

        var accessToken = await _apiService.Object.GetAccessTokenAsync();
        Assert.NotNull(accessToken);
        Assert.Equal(_accessToken, accessToken);
    }

    [Fact]
    public async Task GetAccountRequestAsyncTest()
    {
        var query = "SELECT Id, Name FROM Account LIMIT 1";
        var queryResponse = "{\"totalSize\":1,\"done\":true,\"records\":[{\"attributes\":{\"type\":\"Account\",\"url\":\"/services/data/v58.0/sobjects/Account/001gK000002OgxpQAC\"},\"Id\":\"001gK000002OgxpQAC\",\"Name\":\"Edge Communications\"}]}";

        _apiService.Setup(x => x.GetRequestAsync(_accessToken, "Account", query)).ReturnsAsync(queryResponse);

        var resp = await _apiService.Object.GetRequestAsync(_accessToken, "Account", query);
        Assert.NotNull(resp);
        Assert.Equal(queryResponse, resp);
    }

    [Fact]
    public async Task PostAccountRequesAsyncTest()
    {
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Name\":\"CH Test Account 2\",\"Phone\":\"555-444-5855\",\"Description\":\"CH Test Account 2 Creation\"}]";

        _apiService.Setup(x => x.PostRequesAsynct(_accessToken, "Account", body)).ReturnsAsync(_operationResponse);

        var resp = await _apiService.Object.PostRequesAsynct(_accessToken, "Account", body);
        Assert.NotNull(resp);
        Assert.Equal(_operationResponse, resp);
    }

    [Fact]
    public async Task PatchRequestAsyncTest()
    {
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Id\": \"001gK00000391O9QAI\", \"Description\":\"CH Test Account Update\"}]";

        _apiService.Setup(x => x.PatchRequestAsync(_accessToken, "Account", body)).ReturnsAsync(_operationResponse);

        var resp = await _apiService.Object.PatchRequestAsync(_accessToken, "Account", body);
        Assert.NotNull(resp);
        Assert.Equal(_operationResponse, resp);
    }

    [Fact]
    public async Task UpsertUpdateRequestAsyncTest()
    {
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Id\": \"001gK00000391O9QAI\", \"Description\":\"CH Test Account Upsert\"}]";

        _apiService.Setup(x => x.UpsertRequestAsync(_accessToken, "Account", body, "Id")).ReturnsAsync(_operationResponse);

        var resp = await _apiService.Object.UpsertRequestAsync(_accessToken, "Account", body, "Id");
        Assert.NotNull(resp);
        Assert.Equal(_operationResponse, resp);
    }

    [Fact]
    public async Task UpsertCreateRequestAsyncTest()
    {
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Name\": \"CH Test Account 3\", \"Description\":\"CH Test Account New\"}]";

        _apiService.Setup(x => x.UpsertRequestAsync(_accessToken, "Account", body, "Id")).ReturnsAsync(_operationResponse);

        var resp = await _apiService.Object.UpsertRequestAsync(_accessToken, "Account", body, "Id");
        Assert.NotNull(resp);
        Assert.Equal(_operationResponse, resp);
    }

    [Fact]
    public async Task UpsertBothRequestAsyncTest()
    {
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Id\": \"001gK00000391O9QAI\", \"Description\":\"CH Test Account Upsert\"},{\"attributes\":{\"type\":\"Account\"},\"Name\": \"CH Test Account 4\", \"Description\":\"CH Test Account 4 New\"}]";

        _apiService.Setup(x => x.UpsertRequestAsync(_accessToken, "Account", body, "Id")).ReturnsAsync(_operationResponse);

        var resp = await _apiService.Object.UpsertRequestAsync(_accessToken, "Account", body, "Id");
        Assert.NotNull(resp);
        Assert.Equal(_operationResponse, resp);
    }
}
