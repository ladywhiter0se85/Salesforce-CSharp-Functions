using System.Net;
using Salesforce_Functions.Exceptions;
using Salesforce_Functions.Models.Responses;

namespace Salesforce_Functions.Models.Validation
{
    public class ParameterValidation
    {
        public static string Validate(string? value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ApiResponseException<string>(
                    new ApiResponse<string>(HttpStatusCode.BadRequest, $"The parameter '{paramName}' is required."));
            }
            return value!;
        }

        public static bool Validate(bool? value, string paramName)
        {
            if (value == null)
            {
                throw new ApiResponseException<string>(
                    new ApiResponse<string>(HttpStatusCode.BadRequest, $"The parameter '{paramName}' is required."));
            }

            return value.Value;
        }
    }
}