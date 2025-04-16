namespace Salesforce_Functions
{
	public class Contact
	{
        public Attributes Attributes { get; set; } = new Attributes { Type = "Contact" };
        public string? Id { get; set; }
        public bool? IsDeleted { get; set; }
        public Contact? MasterRecordId { get; set; }
        public object? AccountId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Salutation { get; set; }
        public string? Name { get; set; }
        public string? OtherStreet { get; set; }
        public string? OtherCity { get; set; }
        public string? OtherState { get; set; }
        public string? OtherPostalCode { get; set; }
        public string? OtherCountry { get; set; }
        public string? OtherStateCode { get; set; }
        public string? OtherCountryCode { get; set; }
        public double? OtherLatitude { get; set; }
        public double? OtherLongitude { get; set; }
        public string? OtherGeocodeAccuracy { get; set; }
        public Address? OtherAddress { get; set; }
        public string? MailingStreet { get; set; }
        public string? MailingCity { get; set; }
        public string? MailingState { get; set; }
        public string? MailingPostalCode { get; set; }
        public string? MailingCountry { get; set; }
        public string? MailingStateCode { get; set; }
        public string? MailingCountryCode { get; set; }
        public double? MailingLatitude { get; set; }
        public double? MailingLongitude { get; set; }
        public string? MailingGeocodeAccuracy { get; set; }
        public Address? MailingAddress { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? MobilePhone { get; set; }
        public string? HomePhone { get; set; }
        public string? OtherPhone { get; set; }
        public string? AssistantPhone { get; set; }
        public Contact? ReportsToId { get; set; }
        public string? Email { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? AssistantName { get; set; }
        public string? LeadSource { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Description { get; set; }
        public object? OwnerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public object? CreatedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public object? LastModifiedById { get; set; }
        public DateTime? SystemModstamp { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastCURequestDate { get; set; }
        public DateTime? LastCUUpdateDate { get; set; }
        public DateTime? LastViewedDate { get; set; }
        public DateTime? LastReferencedDate { get; set; }
        public string? EmailBouncedReason { get; set; }
        public DateTime? EmailBouncedDate { get; set; }
        public bool? IsEmailBounced { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Jigsaw { get; set; }
        public string? JigsawContactId { get; set; }
        public string? CleanStatus { get; set; }
        public object? IndividualId { get; set; }
        public string? Level__c { get; set; }
        public string? Languages__c { get; set; }
    }
}
