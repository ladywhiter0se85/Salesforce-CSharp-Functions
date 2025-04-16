namespace Tests.Fixtures
{
    public class OpportunityApiServiceLiveFixture : IDisposable
    {
        public ILogger Logger { get; }
        public OpportunityApiService OpportunityApiService { get; }

        public OpportunityApiServiceLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger>();
            Logger = logger.Object;

            OpportunityApiService = new OpportunityApiService(Logger);
        }

        public void Dispose()
        {
        }
    }
}