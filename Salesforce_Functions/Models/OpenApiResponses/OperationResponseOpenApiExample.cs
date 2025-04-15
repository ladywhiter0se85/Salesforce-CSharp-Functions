using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Salesforce_Functions.Models.Responses;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Models
{
    public class OperationResponseOpenApiExample : OpenApiExample<List<OperationResponse>>
    {
        public override IOpenApiExample<List<OperationResponse>> Build(NamingStrategy namingStrategy)
        {
            string accountsExampleJson = "Resources/OpenApiExamples/operationsResponseOASExample.json";
            var accountsExample = ResponseUtility.ReadFileToCompactJson<List<OperationResponse>>(accountsExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", accountsExample));
            return this;
        }
    }
}