using System.Net;

namespace Salesforce_Functions.Models.Responses
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T? data)
        {
            StatusCode = HttpStatusCode.OK;
            Data = data;
        }

        public ApiResponse(HttpStatusCode statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}