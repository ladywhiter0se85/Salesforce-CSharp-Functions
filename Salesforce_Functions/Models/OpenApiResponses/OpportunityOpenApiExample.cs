using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Models
{
    public class OpportunityOpenApiExample : OpenApiExample<Opportunity>
    {
        public override IOpenApiExample<Opportunity> Build(NamingStrategy namingStrategy)
        {
            string opportunityExampleJson = "Resources/OpenApiExamples/opportunityOASExample.json";
            var opportunityExample = ResponseUtility.ReadFileToCompactJson<Opportunity>(opportunityExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", opportunityExample));
            return this;
        }
    }
    public class OpportunitiesOpenApiExample : OpenApiExample<List<Opportunity>>
    {
        public override IOpenApiExample<List<Opportunity>> Build(NamingStrategy namingStrategy)
        {
            string opportunitiesExampleJson = "Resources/OpenApiExamples/opportunitiesOASExample.json";
            var opportunitiesExample = ResponseUtility.ReadFileToCompactJson<List<Opportunity>>(opportunitiesExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", opportunitiesExample));
            return this;
        }
    }
}