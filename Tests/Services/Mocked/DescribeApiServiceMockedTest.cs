namespace Tests.Services.Mocked
{
    public class DescribeApiServiceMockedTest(DescribeApiServiceMockedFixture describeApiServiceFixture) : IClassFixture<DescribeApiServiceMockedFixture>
    {
        public Mock<IDescribeApiService> _describeApiService = describeApiServiceFixture.DescribeApiService;

        [Fact]
        public async Task GetDescribesAsyncTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/Describe/describesResponse.json");
            var describes = JsonConvert.DeserializeObject<Dictionary<string, List<DescribeField>>>(json);

            _describeApiService.Setup(x => x.GetDescribesAsync()).ReturnsAsync(new ApiResponse<Dictionary<string, List<DescribeField>>>(describes));

            var resp = await _describeApiService.Object.GetDescribesAsync();
            Assert.NotNull(resp);
            Assert.NotNull(resp.Data);
            Assert.Equal("Contact ID", resp.Data["contact"][0].Label);
        }
    }
}