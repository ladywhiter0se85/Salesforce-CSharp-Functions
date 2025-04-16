namespace Salesforce_Functions.Queries
{
    public class OpportunityQueries
    {
        public static string GetAllOpportunitiesQuery => $"{SharedFields}";
        public static string GetOpportunityByIdQuery => $"{SharedFields} WHERE Id=";
        public static string GetOpportunitiesByFilterQuery => $"{SharedFields} WHERE ";
        private static string SharedFields => "SELECT Id, Name, AccountId, Amount, CloseDate, Description, TotalOpportunityQuantity, StageName, Type FROM Opportunity";
    }
}