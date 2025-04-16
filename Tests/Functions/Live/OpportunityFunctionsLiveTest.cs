namespace Tests.Functions.Live
{
    public class OpportunityFuctionsLiveTest(OpportunityFunctionsLiveFixture opportunityFunctionsFixture) : IClassFixture<OpportunityFunctionsLiveFixture>
    {
        public OpportunityFunctions _opportunityFunctions = opportunityFunctionsFixture.OpportunityFunctions;

        [Fact]
        public async Task GetOpportunitiesTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.GetOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Opportunity>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunityByIdTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.GetOpportunityById(request, "006gK000000J5DhQAK");
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Opportunity>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunityByIdNoOpportunityIdTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.GetOpportunityById(request, "");
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'opportunityId' is required.", resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.GetOpportunitiesByFilter(request, "StageName:Closed Won&Type:New Customer", true);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Opportunity>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterNoFilterTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.GetOpportunitiesByFilter(request, "", true);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'where' is required.", resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterNoIsAndTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.GetOpportunitiesByFilter(request, "StageName:Closed Won&Type:New Customer", null);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'isAnd' is required.", resp);
        }

        [Fact]
        public async Task PostOpportunitiesTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/postOpportunitiesRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var opportunitiesResponse = await _opportunityFunctions.PostOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PostOpportunitiesNoBodyTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.PostOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        [Fact]
        public async Task PatchOpportunitiesTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/patchOpportunitiesRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var opportunitiesResponse = await _opportunityFunctions.PatchOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PatchOpportunitiesNoBodyTest()
        {
            Initialize();

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.PatchOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        [Fact]
        public async Task PatchOpportunitiesNoIdInBodyTest()
        {
            Initialize();
            var body = "[{\"attributes\":{\"type\":\"Opportunity\"}, \"Description\":\"CH Test Opportunity Update\"}]";

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var opportunitiesResponse = await _opportunityFunctions.PatchOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Each object in a PATCH request must include an 'Id' field.", resp);
        }

        [Fact]
        public async Task PutOpportunitiesTest()
        {
            Initialize();
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/upsertOpportunitiesRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var opportunitiesResponse = await _opportunityFunctions.PutOpportunities(request, externalField);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PutOpportunitiesNoExternalFieldTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/upsertOpportunitiesRequest.json");

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            var opportunitiesResponse = await _opportunityFunctions.PutOpportunities(request, "");
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The parameter 'externalField' is required.", resp);
        }

        [Fact]
        public async Task PutOpportunitiesNoBodyTest()
        {
            Initialize();
            var externalField = "Id";

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            var opportunitiesResponse = await _opportunityFunctions.PutOpportunities(request, externalField);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("The request body is required.", resp);
        }

        private void Initialize()
        {
            //Verify OpportunityFunctions
            Assert.NotNull(_opportunityFunctions);
        }
    }
}