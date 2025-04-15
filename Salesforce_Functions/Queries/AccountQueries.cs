namespace Salesforce_Functions.Queries
{
    public class AccountQueries
    {
        public static string GetAllAccountsQuery => $"{SharedFields}";
        public static string GetAccountByIdQuery => $"{SharedFields} WHERE Id=";
        public static string GetAccountByFilterQuery => $"{SharedFields} WHERE ";
        private static string SharedFields => "SELECT Id, Name, Active__c, BillingAddress, Description, Phone, Type FROM Account";
    }
}