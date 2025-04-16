namespace Tests.Services.Mocked
{
    public class OpportunityApiServiceMockedTest(OpportunityApiServiceMockedFixture opportunityApiServiceFixture) : IClassFixture<OpportunityApiServiceMockedFixture>
    {
        public Mock<IOpportunityApiService> _opportunityApiService = opportunityApiServiceFixture.OpportunityApiService;

        [Fact]
        public async Task GetAllOpportunitiesAsyncTest()
        {
            var json = ResponseUtility.ReadFile("Resources/Mocked/opportunitiesResponse.json");
            var opportunitys = JsonConvert.DeserializeObject<List<Opportunity>>(json);

            _opportunityApiService.Setup(x => x.GetAllOpportunitiesAsync()).ReturnsAsync(new ApiResponse<List<Opportunity>>(opportunitys));

            var resp = await _opportunityApiService.Object.GetAllOpportunitiesAsync();
            Assert.NotNull(resp);
            Assert.Equal("Mocked Opportunity One", resp.Data![0].Name);
        }

        [Fact]
        public async Task GetOpportunityByIdAsyncTest()
        {
            var opportunityId = "mockedOpportunityTwoId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/opportunityResponse.json");
            var opportunity = JsonConvert.DeserializeObject<Opportunity>(json)!;
            Assert.NotNull(opportunity);

            _opportunityApiService.Setup(x => x.GetOpportunityByIdAsync(opportunityId)).ReturnsAsync(new ApiResponse<Opportunity>(opportunity));

            ApiResponse<Opportunity> resp = await _opportunityApiService.Object.GetOpportunityByIdAsync(opportunityId)!;
            Assert.NotNull(resp);
            Assert.NotNull(resp.Data);
            Assert.Equal("Mocked Opportunity One", resp.Data!.Name);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterAsyncOneToOneAndTest()
        {
            var filter = "StageName:Qualification&Type:Existing Customer - Upgrade";
            var json = ResponseUtility.ReadFile("Resources/Mocked/opportunitiesResponse.json");
            var opportunitys = JsonConvert.DeserializeObject<List<Opportunity>>(json);

            _opportunityApiService.Setup(x => x.GetOpportunitiesByFilterAsync(filter, true)).ReturnsAsync(new ApiResponse<List<Opportunity>>(opportunitys));

            var resp = await _opportunityApiService.Object.GetOpportunitiesByFilterAsync(filter, true);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task GetOpportunitiesByFilterAsyncOneToManyTest()
        {
            var filter = "StageName:Qualification,Negotiation/Review";
            var json = ResponseUtility.ReadFile("Resources/Mocked/opportunitiesResponse.json");
            var opportunitys = JsonConvert.DeserializeObject<List<Opportunity>>(json);

            _opportunityApiService.Setup(x => x.GetOpportunitiesByFilterAsync(filter, true)).ReturnsAsync(new ApiResponse<List<Opportunity>>(opportunitys));

            var resp = await _opportunityApiService.Object.GetOpportunitiesByFilterAsync(filter, true);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task GetOpportunitiesByFilterAsyncManyToOneOrTest()
        {
            var filter = "Name:Mocked Opportunity One,Mocked Opportunity Two&Id:mockedOpportunityTwoId";
            var json = ResponseUtility.ReadFile("Resources/Mocked/opportunitiesResponse.json");
            var opportunitys = JsonConvert.DeserializeObject<List<Opportunity>>(json);

            _opportunityApiService.Setup(x => x.GetOpportunitiesByFilterAsync(filter, false)).ReturnsAsync(new ApiResponse<List<Opportunity>>(opportunitys));

            var resp = await _opportunityApiService.Object.GetOpportunitiesByFilterAsync(filter, false);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }

        [Fact]
        public async Task PostOpportunitiesAsyncTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/postOpportunitiesRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);

            _opportunityApiService.Setup(x => x.PostOpportunitiesAsync(body!)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _opportunityApiService.Object.PostOpportunitiesAsync(body!);
            Assert.NotNull(resp);
            Assert.Single(resp.Data!);
        }

        [Fact]
        public async Task PatchOpportunitiesAsyncTest()
        {
            var body = ResponseUtility.ReadFile("Resources/Live/patchOpportunitiesRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);

            _opportunityApiService.Setup(x => x.PatchOpportunitiesAsync(body!)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _opportunityApiService.Object.PatchOpportunitiesAsync(body!);
            Assert.NotNull(resp);
            Assert.Single(resp.Data!);
        }

        [Fact]
        public async Task UpsertOpportunitiesAsyncTest()
        {
            var externalField = "Id";
            var body = ResponseUtility.ReadFile("Resources/Live/upsertOpportunitiesRequest.json");
            var opJson = ResponseUtility.ReadFile("Resources/Mocked/operationResponse.json");
            var operationResp = JsonConvert.DeserializeObject<List<OperationResponse>>(opJson);
            operationResp!.Add(new OperationResponse() { Id = "mockedOpportunityIdTwo" });

            _opportunityApiService.Setup(x => x.UpsertOpportunitiesAsync(body!, externalField)).ReturnsAsync(new ApiResponse<List<OperationResponse>>(operationResp));

            var resp = await _opportunityApiService.Object.UpsertOpportunitiesAsync(body!, externalField);
            Assert.NotNull(resp);
            Assert.Equal(2, resp.Data!.Count());
        }
    }
}