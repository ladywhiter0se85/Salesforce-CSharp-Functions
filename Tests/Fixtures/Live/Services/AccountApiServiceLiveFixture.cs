namespace Tests.Fixtures
{
    public class AccountApiServiceLiveFixture : IDisposable
    {
        public ILogger Logger { get; }
        public AccountApiService AccountApiService { get; }

        public AccountApiServiceLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger>();
            Logger = logger.Object;

            AccountApiService = new AccountApiService(Logger);
        }

        public void Dispose()
        {
        }
    }
}