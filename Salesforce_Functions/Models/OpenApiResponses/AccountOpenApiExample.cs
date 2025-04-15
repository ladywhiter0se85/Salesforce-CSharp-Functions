using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Models
{
    public class AccountOpenApiExample : OpenApiExample<Account>
    {
        public override IOpenApiExample<Account> Build(NamingStrategy namingStrategy)
        {
            string accountsExampleJson = "Resources/OpenApiExamples/accountOASExample.json";
            var accountsExample = ResponseUtility.ReadFileToCompactJson<Account>(accountsExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", accountsExample));
            return this;
        }
    }
    public class AccountsOpenApiExample : OpenApiExample<List<Account>>
    {
        public override IOpenApiExample<List<Account>> Build(NamingStrategy namingStrategy)
        {
            string accountsExampleJson = "Resources/OpenApiExamples/accountsOASExample.json";
            var accountsExample = ResponseUtility.ReadFileToCompactJson<List<Account>>(accountsExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", accountsExample));
            return this;
        }
    }
}