namespace Tests.Fixtures
{
    public class OpportunityFunctionsLiveFixture : IDisposable
    {
        public ILogger<OpportunityFunctions> Logger { get; }
        public OpportunityFunctions OpportunityFunctions { get; }

        public OpportunityFunctionsLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger<OpportunityFunctions>>();
            Logger = logger.Object;

            OpportunityFunctions = new OpportunityFunctions(Logger);
        }

        public void Dispose()
        {
        }
    }
}