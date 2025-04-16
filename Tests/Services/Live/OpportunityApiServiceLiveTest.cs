namespace Tests.Services.Live
{
    public class OpportunityApiServiceLiveTest(OpportunityApiServiceLiveFixture opportunityApiServiceFixture) : IClassFixture<OpportunityApiServiceLiveFixture>
    {
        public OpportunityApiService _opportunityApiService = opportunityApiServiceFixture.OpportunityApiService;

        [Fact]
        public async Task GetAllOpportunitiesAsyncTest()
        {
            Initialize();
            var resp = await _opportunityApiService.GetAllOpportunitiesAsync();
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunityByIdAsyncTest()
        {
            Initialize();
            var resp = await _opportunityApiService.GetOpportunityByIdAsync("006gK000000J5DhQAK");
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterAsyncOneToOneAndTest()
        {
            Initialize();
            var filter = "StageName:Closed Won&Type:New Customer";
            var resp = await _opportunityApiService.GetOpportunitiesByFilterAsync(filter, true);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterAsyncOneToManyTest()
        {
            Initialize();
            var filter = "StageName:Value Proposition,Id. Decision Makers";
            var resp = await _opportunityApiService.GetOpportunitiesByFilterAsync(filter, true);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task GetOpportunitiesByFilterAsyncManyToOneOrTest()
        {
            Initialize();
            var filter = "Name:Grand Hotels SLA,Edge SLA,United Oil SLA&AccountId:001gK000002OgxzQAC";
            var resp = await _opportunityApiService.GetOpportunitiesByFilterAsync(filter, false);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PostOpportunitiesAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/postOpportunitiesRequest.json");
            var resp = await _opportunityApiService.PostOpportunitiesAsync(body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task PatchOpportunitiesAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/patchOpportunitiesRequest.json");
            var resp = await _opportunityApiService.PatchOpportunitiesAsync(body);
            Assert.NotNull(resp);
        }

        [Fact]
        public async Task UpsertOpportunitiesAsyncTest()
        {
            Initialize();
            var body = ResponseUtility.ReadFile("Resources/Live/Opportunity/upsertOpportunitiesRequest.json");
            var externalField = "Id";
            var resp = await _opportunityApiService.UpsertOpportunitiesAsync(body, externalField);
            Assert.NotNull(resp);
        }

        private void Initialize()
        {
            //Verify OpportunityApiService
            Assert.NotNull(_opportunityApiService);
        }
    }
}