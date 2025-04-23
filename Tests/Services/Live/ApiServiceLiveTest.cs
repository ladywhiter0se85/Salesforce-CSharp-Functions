namespace Tests.Services.Live;

public class ApiServiceLiveTest(ApiServiceLiveFixture apiServiceFixture) : IClassFixture<ApiServiceLiveFixture>
{
    public ApiService _apiService = apiServiceFixture.ApiService;
    private string? _accessToken = null;

    [Fact]
    public async Task GetAccessTokenAsyncTest()
    {
        //Verify ApiService
        Assert.NotNull(_apiService);

        var accessToken = await _apiService.GetAccessTokenAsync();
        Assert.NotNull(accessToken);
    }

    [Fact]
    public async Task GetAccountQueryAsyncTest()
    {
        await Initialize();
        var query = "SELECT Id, Name FROM Account LIMIT 1";
        var resp = await _apiService.GetQueryAsync(_accessToken!, "Account", query);
        Assert.NotNull(resp);
    }

    [Fact]
    public async Task GetDescribeAsyncTest()
    {
        await Initialize();
        var resp = await _apiService.GetDescribeAsync(_accessToken!, "Account");
        Assert.NotNull(resp);
    }

    [Fact]
    public async Task PostAccountRequestAsyncTest()
    {
        await Initialize();
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Name\":\"CH Test Account 6\",\"Phone\":\"555-444-5855\",\"Description\":\"CH Test Account 6 Creation\"}]";
        var resp = await _apiService.PostRequesAsync(_accessToken!, "Account", body);
        Assert.NotNull(resp);
    }

    [Fact]
    public async Task PatchRequestAsyncTest()
    {
        await Initialize();
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Id\": \"001gK00000391O9QAI\", \"Description\":\"CH Test Account Update\"}]";
        var resp = await _apiService.PatchRequestAsync(_accessToken!, "Account", body);
        Assert.NotNull(resp);
    }

    [Fact]
    public async Task UpsertUpdateRequestAsyncTest()
    {
        await Initialize();
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Id\": \"001gK00000391O9QAI\", \"Description\":\"CH Test Account Upsert\"}]";
        var resp = await _apiService.UpsertRequestAsync(_accessToken!, "Account", body, "Id");
        Assert.NotNull(resp);
    }

    [Fact]
    public async Task UpsertCreateRequestAsyncTest()
    {
        await Initialize();
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Name\": \"CH Test Account 3\", \"Description\":\"CH Test Account New\"}]";
        var resp = await _apiService.UpsertRequestAsync(_accessToken!, "Account", body, "Id");
        Assert.NotNull(resp);
    }

    [Fact]
    public async Task UpsertBothRequestAsyncTest()
    {
        await Initialize();
        var body = "[{\"attributes\":{\"type\":\"Account\"},\"Id\": \"001gK00000391O9QAI\", \"Description\":\"CH Test Account Upsert\"},{\"attributes\":{\"type\":\"Account\"},\"Name\": \"CH Test Account 4\", \"Description\":\"CH Test Account 4 New\"}]";
        var resp = await _apiService.UpsertRequestAsync(_accessToken!, "Account", body, "Id");
        Assert.NotNull(resp);
    }

    private async Task Initialize()
    {
        //Verify ApiService
        Assert.NotNull(_apiService);

        _accessToken = await _apiService.GetAccessTokenAsync();
        Assert.NotNull(_accessToken);
    }
}
