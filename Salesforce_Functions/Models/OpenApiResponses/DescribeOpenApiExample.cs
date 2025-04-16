using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Models
{
    public class DescribesOpenApiExample : OpenApiExample<Dictionary<string, List<DescribeField>>>
    {
        public override IOpenApiExample<Dictionary<string, List<DescribeField>>> Build(NamingStrategy namingStrategy)
        {
            string describesExampleJson = "Resources/OpenApiExamples/Describe/describesOASExample.json";
            var describesExample = ResponseUtility.ReadFileToCompactJson<Dictionary<string, List<DescribeField>>>(describesExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", describesExample));
            return this;
        }
    }
}