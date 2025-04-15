using System.Net;
using Newtonsoft.Json.Linq;
using Salesforce_Functions.Exceptions;
using Salesforce_Functions.Models.Responses;

namespace Salesforce_Functions.Models.Validation
{
    public class BodyValidation
    {
        public static async Task<string> ValidateAsync(Stream bodyStream)
        {
            using var reader = new StreamReader(bodyStream);
            var body = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ApiResponseException<string>(new ApiResponse<string>(HttpStatusCode.BadRequest, "The request body is required."));
            }

            return body;
        }

        public static async Task<string> ValidateAndCheckIdsAsync(Stream bodyStream)
        {
            using var reader = new StreamReader(bodyStream);
            var body = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ApiResponseException<string>(
                    new ApiResponse<string>(HttpStatusCode.BadRequest, "The request body is required."));
            }

            //Validate for Patch endpoint that each item in the array contains an Id ignoring case
            var array = JArray.Parse(body);
            bool hasMissingId = array.Any(item => string.IsNullOrWhiteSpace(((JObject)item).Properties().FirstOrDefault(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))?.Value?.ToString()));

            if (hasMissingId)
            {
                throw new ApiResponseException<string>(
                    new ApiResponse<string>(HttpStatusCode.BadRequest, "Each object in a PATCH request must include an 'Id' field."));
            }

            return body;
        }
    }
}