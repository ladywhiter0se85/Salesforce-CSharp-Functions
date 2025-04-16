namespace Tests.Fixtures
{
    public interface IDescribeFunctions
    {
        public Task<HttpResponseData> GetDescribes([HttpTrigger(AuthorizationLevel.Function, "get", Route = "describes")] HttpRequestData req, string? sObjects);
    }

    public class DescribeFunctionsMockedFixture : IDisposable
    {
        public Mock<IDescribeFunctions> DescribeFunctions { get; }

        public DescribeFunctionsMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            DescribeFunctions = new Mock<IDescribeFunctions>();
        }

        public void Dispose()
        {
        }
    }
}