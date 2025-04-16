namespace Tests.Fixtures
{
    public class AccountFunctionsLiveFixture : IDisposable
    {
        public ILogger<AccountFunctions> Logger { get; }
        public AccountFunctions AccountFunctions { get; }

        public AccountFunctionsLiveFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            var logger = new Mock<ILogger<AccountFunctions>>();
            Logger = logger.Object;

            AccountFunctions = new AccountFunctions(Logger);
        }

        public void Dispose()
        {
        }
    }
}