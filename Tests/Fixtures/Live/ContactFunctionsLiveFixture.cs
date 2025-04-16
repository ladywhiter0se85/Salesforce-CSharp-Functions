namespace Tests.Fixtures
{
    public class ContactFunctionsLiveFixture : IDisposable
    {
        public ILogger<ContactFunctions> Logger { get; }
        public ContactFunctions ContactFunctions { get; }

        public ContactFunctionsLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger<ContactFunctions>>();
            Logger = logger.Object;

            ContactFunctions = new ContactFunctions(Logger);
        }

        public void Dispose()
        {
        }
    }
}