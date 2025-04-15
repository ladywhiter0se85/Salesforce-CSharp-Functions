using Salesforce_Functions.Models.Responses;

namespace Salesforce_Functions.Exceptions
{
    public class ApiResponseException<T> : Exception
    {
        public ApiResponse<T> ApiResponse { get; }

        public ApiResponseException(ApiResponse<T> apiResponse) : base(apiResponse.Message)  // Pass a message to the base constructor
        {
            ApiResponse = apiResponse;  // Store the full ApiResponse for later use
        }
    }
}