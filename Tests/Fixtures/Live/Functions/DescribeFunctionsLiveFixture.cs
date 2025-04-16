namespace Tests.Fixtures
{
    public class DescribeFunctionsLiveFixture : IDisposable
    {
        public ILogger<DescribeFunctions> Logger { get; }
        public DescribeFunctions DescribeFunctions { get; }

        public DescribeFunctionsLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger<DescribeFunctions>>();
            Logger = logger.Object;

            DescribeFunctions = new DescribeFunctions(Logger);
        }

        public void Dispose()
        {
        }
    }
}