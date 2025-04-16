using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Salesforce_Functions.Utilities;

namespace Salesforce_Functions.Models
{
    public class ContactOpenApiExample : OpenApiExample<Contact>
    {
        public override IOpenApiExample<Contact> Build(NamingStrategy namingStrategy)
        {
            string contactExampleJson = "Resources/OpenApiExamples/Contact/contactOASExample.json";
            var contactExample = ResponseUtility.ReadFileToCompactJson<Contact>(contactExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", contactExample));
            return this;
        }
    }
    public class ContactsOpenApiExample : OpenApiExample<List<Contact>>
    {
        public override IOpenApiExample<List<Contact>> Build(NamingStrategy namingStrategy)
        {
            string contactsExampleJson = "Resources/OpenApiExamples/Contact/contactsOASExample.json";
            var contactsExample = ResponseUtility.ReadFileToCompactJson<List<Contact>>(contactsExampleJson);
            Examples.Add(OpenApiExampleResolver.Resolve("default", contactsExample));
            return this;
        }
    }
}