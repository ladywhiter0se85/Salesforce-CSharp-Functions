using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Salesforce_Functions
{
    public class OpportunityFunctions
    {
        private readonly ILogger<OpportunityFunctions> _logger;

        public OpportunityFunctions(ILogger<OpportunityFunctions> logger)
        {
            _logger = logger;
        }

        [Function("OpportunityFunctions")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
