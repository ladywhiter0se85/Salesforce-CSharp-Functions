namespace Tests.Utilities;

public class ConfigurationUtilityTest
{
    [Fact]
    public void GetApiPropertiesTest()
    {
        ConfigurationUtility.IsTestEnvironment = true;
        var apiProperties = ConfigurationUtility.GetApiProperties();
        Assert.NotNull(apiProperties);
    }
}