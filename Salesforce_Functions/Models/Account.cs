namespace Salesforce_Functions
{
	public class Account
	{
        public Attributes Attributes { get; set; } = new Attributes { Type = "Account" };
        public string? Id { get; set; }
        public bool? IsDeleted { get; set; }
        public Account? MasterRecordId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public Account? ParentId { get; set; }
        public string? BillingStreet { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingPostalCode { get; set; }
        public string? BillingCountry { get; set; }
        public string? BillingStateCode { get; set; }
        public string? BillingCountryCode { get; set; }
        public double? BillingLatitude { get; set; }
        public double? BillingLongitude { get; set; }
        public string? BillingGeocodeAccuracy { get; set; }
        public Address? BillingAddress { get; set; }
        public string? ShippingStreet { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingState { get; set; }
        public string? ShippingPostalCode { get; set; }
        public string? ShippingCountry { get; set; }
        public string? ShippingStateCode { get; set; }
        public string? ShippingCountryCode { get; set; }
        public double? ShippingLatitude { get; set; }
        public double? ShippingLongitude { get; set; }
        public string? ShippingGeocodeAccuracy { get; set; }
        public Address? ShippingAddress { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? AccountNumber { get; set; }
        public string? Website { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Sic { get; set; }
        public string? Industry { get; set; }
        public double? AnnualRevenue { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string? Ownership { get; set; }
        public string? TickerSymbol { get; set; }
        public string? Description { get; set; }
        public string? Rating { get; set; }
        public string? Site { get; set; }
        public object? OwnerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public object? CreatedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public object? LastModifiedById { get; set; }
        public DateTime? SystemModstamp { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastViewedDate { get; set; }
        public DateTime? LastReferencedDate { get; set; }
        public string? Jigsaw { get; set; }
        public string? JigsawCompanyId { get; set; }
        public string? CleanStatus { get; set; }
        public string? AccountSource { get; set; }
        public string? DunsNumber { get; set; }
        public string? Tradestyle { get; set; }
        public string? NaicsCode { get; set; }
        public string? NaicsDesc { get; set; }
        public string? YearStarted { get; set; }
        public string? SicDesc { get; set; }
        public object? DandbCompanyId { get; set; }
        public object? OperatingHoursId { get; set; }
        public string? CustomerPriority__c { get; set; }
        public string? SLA__c { get; set; }
        public string? Active__c { get; set; }
        public double? NumberofLocations__c { get; set; }
        public string? UpsellOpportunity__c { get; set; }
        public string? SLASerialNumber__c { get; set; }
        public DateTime? SLAExpirationDate__c { get; set; }
    }
}
