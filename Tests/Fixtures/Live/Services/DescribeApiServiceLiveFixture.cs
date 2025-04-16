namespace Tests.Fixtures
{
    public class DescribeApiServiceLiveFixture : IDisposable
    {
        public ILogger Logger { get; }
        public DescribeApiService DescribeApiService { get; }

        public DescribeApiServiceLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger>();
            Logger = logger.Object;

            DescribeApiService = new DescribeApiService(Logger);
        }

        public void Dispose()
        {
        }
    }
}