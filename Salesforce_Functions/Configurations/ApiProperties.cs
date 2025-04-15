namespace Salesforce_Functions.Configurations
{
    public class ApiProperties
    {
        public required string EncryptionKey { get; set; }
        public required string EncryptionIV { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string SecretToken { get; set; }
        public required string LoginUrl { get; set; }
        public required string DomainUrl { get; set; }
        public required string Version { get; set; }
    }
}