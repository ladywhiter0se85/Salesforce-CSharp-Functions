
using Salesforce_Functions.Configurations;

namespace Tests.Utilities;
public class EncryptionUtilityTest
{
    private readonly string key = "QWd1zkYA1ucX265/ks/EUA7S0KD7PaB2X32/On3w5So=";
    private readonly string iv = "ibW0CnrwwJsZySyEQZg41w==";

    [Fact]
    public void EncryptTest()
    {
        var plainString = "testClientId";
        var encryptedText = EncryptionUtility.Encrypt(key, iv, plainString);
        Assert.NotNull(encryptedText);
    }

    [Fact]
    public void DecryptTest()
    {
        var plainString = "testClientId";
        var encryptedText = "TS+x9aXfB4mv4nxpsUTa+g==";
        var decryptedText = EncryptionUtility.Decrypt(key, iv, encryptedText);
        Assert.NotNull(decryptedText);
        Assert.Equal(plainString, decryptedText);
    }

    [Fact]
    public void GenerateEncryptionKey()
    {
        var key = Convert.ToBase64String(Aes.Create().Key);
        var iv = Convert.ToBase64String(Aes.Create().IV);
        Console.WriteLine($"Key: {key}");
        Console.WriteLine($"IV: {iv}");
    }

    [Fact]
    public void EncryptApiProperties()
    {
        var apiProperties = new ApiProperties
        {
            EncryptionKey = "...",
            EncryptionIV = "...",
            ClientId = EncryptionUtility.Encrypt(key, iv, "thisIsNotGoingToBeARealClientIdJustMockedForRepo"),
            ClientSecret = EncryptionUtility.Encrypt(key, iv, "thisIsNotGoingToBeARealClientSecretJustMockedForRepo"),
            Username = EncryptionUtility.Encrypt(key, iv, "mockedSalesforceAccount@mock.com"),
            Password = EncryptionUtility.Encrypt(key, iv, "mockedSalesforceAccountPassword"),
            SecretToken = EncryptionUtility.Encrypt(key, iv, "mockedSalesforceAccountSecretToken"),
            LoginUrl = "...",
            DomainUrl = "...",
            Version = "..."
        };

        var encryptedJson = JsonConvert.SerializeObject(apiProperties);
        Assert.NotNull(encryptedJson);
    }
}