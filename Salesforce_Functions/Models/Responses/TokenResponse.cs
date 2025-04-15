
using Newtonsoft.Json;

namespace Salesforce_Functions.Models.Responses
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public required string AccessToken { get; set; }
    }
}