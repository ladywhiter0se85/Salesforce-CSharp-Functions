namespace Salesforce_Functions.Models.Responses
{
    public class OperationResponse
    {
        public required string Id { get; set; }
        public bool Success { get; set; }
        public bool Created { get; set; }
    }
}