using Newtonsoft.Json;
namespace Salesforce_Functions
{
	public class Address
	{
        public Attributes Attributes { get; set; } = new Attributes { Type = "Address" };
        public string? Id { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public object? CreatedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public object? LastModifiedById { get; set; }
        public DateTime? SystemModstamp { get; set; }
        public object? ParentId { get; set; }
        public string? LocationType { get; set; }
        public string? AddressType { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? StateCode { get; set; }
        public string? CountryCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GeocodeAccuracy { get; set; }
        [JsonProperty("Address")]
        public object? AddressObject { get; set; }
        public string? Description { get; set; }
        public string? DrivingDirections { get; set; }
        public string? TimeZone { get; set; }
    }
}
