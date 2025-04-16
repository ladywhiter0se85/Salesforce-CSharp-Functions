namespace Tests.Fixtures
{
    public class ContactApiServiceLiveFixture : IDisposable
    {
        public ILogger Logger { get; }
        public ContactApiService ContactApiService { get; }

        public ContactApiServiceLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger>();
            Logger = logger.Object;

            ContactApiService = new ContactApiService(Logger);
        }

        public void Dispose()
        {
        }
    }
}