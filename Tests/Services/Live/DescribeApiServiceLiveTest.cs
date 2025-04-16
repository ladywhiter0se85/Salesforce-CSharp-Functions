namespace Tests.Services.Live
{
    public class DescribeApiServiceLiveTest(DescribeApiServiceLiveFixture describeApiServiceFixture) : IClassFixture<DescribeApiServiceLiveFixture>
    {
        public DescribeApiService _describeApiService = describeApiServiceFixture.DescribeApiService;

        [Fact]
        public async Task GetDescribesAsyncTest()
        {
            Initialize();
            var resp = await _describeApiService.GetDescribesAsync(new List<string>() { "Account", "Contact" });
            Assert.NotNull(resp);
        }

        private void Initialize()
        {
            //Verify DescribeApiService
            Assert.NotNull(_describeApiService);
        }
    }
}