namespace Tests.Fixtures
{
    public interface IDescribeApiService
    {
        public Task<ApiResponse<Dictionary<string, List<DescribeField>>>> GetDescribesAsync();
    }

    public class DescribeApiServiceMockedFixture : IDisposable
    {
        public Mock<IDescribeApiService> DescribeApiService { get; }

        public DescribeApiServiceMockedFixture()
        {
            ConfigurationUtility.IsTestEnvironment = true;

            DescribeApiService = new Mock<IDescribeApiService>();
        }

        public void Dispose()
        {
        }
    }
}