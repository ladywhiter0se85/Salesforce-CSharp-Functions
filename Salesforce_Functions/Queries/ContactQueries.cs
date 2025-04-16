namespace Salesforce_Functions.Queries
{
    public static class ContactQueries
    {
        public static string GetAllContactsQuery => $"{SharedFields}";
        public static string GetContactByIdQuery => $"{SharedFields} WHERE Id = ";
        public static string GetContactsByFilterQuery => $"{SharedFields} WHERE ";
        private static string SharedFields => "SELECT Id, Salutation, FirstName, LastName, Name, AccountId, Email, Phone, Description, MailingAddress FROM Contact";
    }

}