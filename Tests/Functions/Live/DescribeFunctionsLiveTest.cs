namespace Tests.Functions.Live
{
    public class DescribeFuctionsLiveTest(DescribeFunctionsLiveFixture describeFunctionsFixture) : IClassFixture<DescribeFunctionsLiveFixture>
    {
        public DescribeFunctions _describeFunctions = describeFunctionsFixture.DescribeFunctions;

        [Fact]
        public async Task GetDescribesTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var describesResponse = await _describeFunctions.GetDescribes(request, "Account,Opportunity");
            Assert.NotNull(describesResponse);
            Assert.Equal(HttpStatusCode.OK, describesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Dictionary<string, List<DescribeField>>>(describesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetDescribesNoSObjectsTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var describesResponse = await _describeFunctions.GetDescribes(request, "");
            Assert.NotNull(describesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, describesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(describesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'sObjects' is required.", resp);
        }

        private void Initialize()
        {
            //Verify DescribeFunctions
            Assert.NotNull(_describeFunctions);
        }
    }
}