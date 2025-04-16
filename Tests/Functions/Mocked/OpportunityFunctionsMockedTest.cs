namespace Tests.Functions.Mocked
{
    public class OpportunityFuctionsTest(OpportunityFunctionsMockedFixture opportunityFunctionsFixture) : IClassFixture<OpportunityFunctionsMockedFixture>
    {
        public Mock<IOpportunityFunctions> _opportunityFunctions = opportunityFunctionsFixture.OpportunityFunctions;

        [Fact]
        public async Task GetOpportunitiesTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/Opportunity/opportunitiesResponse.json");
            var opportunities = JsonConvert.DeserializeObject<List<Opportunity>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, opportunities);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.GetOpportunities(request)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.GetOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Opportunity>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(125000, resp[1].Amount);
        }

        [Fact]
        public async Task GetOpportunityByIdTest()
        {
            var opportunityId = "mockedOpportunityOneId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/Opportunity/opportunitiesResponse.json");
            var opportunities = JsonConvert.DeserializeObject<List<Opportunity>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, opportunities![0]);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.GetOpportunityById(request, opportunityId)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.GetOpportunityById(request, opportunityId);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<Opportunity>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("Qualification", resp.StageName);
        }

        [Fact]
        public async Task GetOpportunityByIdNoOpportunityIdTest()
        {
            var msg = "The parameter 'opportunityId' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.GetOpportunityById(request, "")).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.GetOpportunityById(request, "");
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterTest()
        {
            var filter = "Id:mockedOpportunityOneId";
            var isAnd = true;
            var json = ResponseUtility.ReadFile("Resources/Mocked/Opportunity/opportunitiesResponse.json");
            var opportunities = JsonConvert.DeserializeObject<List<Opportunity>>(json);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, opportunities);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.GetOpportunitiesByFilter(request, filter, isAnd)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.GetOpportunitiesByFilter(request, filter, isAnd);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<Opportunity>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("New Customer", resp[0].Type);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterNoFilterTest()
        {
            var isAnd = true;
            var msg = "The parameter 'where' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.GetOpportunitiesByFilter(request, "", isAnd)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.GetOpportunitiesByFilter(request, "", isAnd);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterNoIsAndTest()
        {
            var filter = "Id:mockedOpportunityOneId";
            var msg = "The parameter 'isAnd' is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.GetOpportunitiesByFilter(request, filter, null)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.GetOpportunitiesByFilter(request, filter, null);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PostOpportunitiesTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/postOpportunitiesRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _opportunityFunctions.Setup(x => x.PostOpportunities(request)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PostOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedSObjectId", resp[0].Id);
        }

        [Fact]
        public async Task PostOpportunitiesNoBodyTest()
        {
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.PostOpportunities(request)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PostOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PatchOpportunitiesTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/patchOpportunitiesRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _opportunityFunctions.Setup(x => x.PatchOpportunities(request)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PatchOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedSObjectId", resp[0].Id);
        }

        [Fact]
        public async Task PatchOpportunitiesNoBodyTest()
        {
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.PatchOpportunities(request)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PatchOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PatchOpportunitiesNoIdInBodyTest()
        {
            var msg = "Each object in a PATCH request must include an 'Id' field.";
            var body = "[{\"attributes\":{\"type\":\"Opportunity\"}, \"Description\":\"CH Test Opportunity Update\"}]";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _opportunityFunctions.Setup(x => x.PatchOpportunities(request)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PatchOpportunities(request);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PutOpportunitiesTest()
        {
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/upsertOpportunitiesRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.OK, operationResp);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _opportunityFunctions.Setup(x => x.PutOpportunities(request, externalField)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PutOpportunities(request, externalField);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.OK, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<List<OperationResponse>>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal("mockedSObjectId", resp[0].Id);
        }

        [Fact]
        public async Task PutOpportunitiesNoExternalFieldTest()
        {
            var msg = "The parameter 'externalField' is required.";
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/upsertOpportunitiesRequest.json");
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, body);
            _opportunityFunctions.Setup(x => x.PutOpportunities(request, "")).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PutOpportunities(request, "");
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }

        [Fact]
        public async Task PutOpportunitiesNoBodyTest()
        {
            var externalField = "Id";
            var msg = "The request body is required.";
            var response = FunctionsUtility.MockedHttpResponseData(HttpStatusCode.BadRequest, msg);

            var request = FunctionsUtility.MockedHttpRequestData(null, null, "");
            _opportunityFunctions.Setup(x => x.PutOpportunities(request, externalField)).ReturnsAsync(response);

            var opportunitiesResponse = await _opportunityFunctions.Object.PutOpportunities(request, externalField);
            Assert.NotNull(opportunitiesResponse);
            Assert.Equal(HttpStatusCode.BadRequest, opportunitiesResponse.StatusCode);

            var resp = await FunctionsUtility.GetBodyFromStream<string>(opportunitiesResponse.Body);
            Assert.NotNull(resp);
            Assert.Equal(msg, resp);
        }
    }
}