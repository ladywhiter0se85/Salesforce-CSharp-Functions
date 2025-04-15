namespace Tests.Fixtures
{
    public class ApiServiceLiveFixture : IDisposable
    {
        public ILogger Logger { get; }
        public ApiService ApiService { get; }

        public ApiServiceLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger>();
            Logger = logger.Object;

            ApiService = new ApiService(Logger);
        }

        public void Dispose()
        {
        }
    }
}