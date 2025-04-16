namespace Tests.Functions.Mocked
{
    public class DescribeFuctionsTest(DescribeFunctionsMockedFixture describeFunctionsFixture) : IClassFixture<DescribeFunctionsMockedFixture>
    {
        public Mock<IDescribeFunctions> _describeFunctions = describeFunctionsFixture.DescribeFunctions;

        [Fact]
        public async Task GetDescribesByFilterTest()
        {
            var sObjects = "Account,Contact";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Describe/describesResponse.json");
            var describes = JsonConvert.DeserializeObject<Dictionary<string, List<DescribeField>>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, describes);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _describeFunctions.Setup(x => x.GetDescribes(request, sObjects)).ReturnsAsync(response);

            var describesResponse = await _describeFunctions.Object.GetDescribes(request, sObjects);
            Assert.NotNull(describesResponse);
            Assert.Equal(HttpStatusCode.OK, describesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Dictionary<string, List<DescribeField>>>(describesResponse.Body);
            Assert.NotNull(resp);;
            Assert.Equal("Account ID", resp["account"][0].Label);
        }

        [Fact]
        public async Task GetDescribesByFilterNoFilterTest()
        {
            var msg = "The parameter 'sObjects' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _describeFunctions.Setup(x => x.GetDescribes(request, "")).ReturnsAsync(response);

            var describesResponse = await _describeFunctions.Object.GetDescribes(request, "");
            Assert.NotNull(describesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, describesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(describesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }
    }
}