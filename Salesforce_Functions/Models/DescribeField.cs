namespace Salesforce_Functions
{
    public class DescribeField
    {
        public required string Name { get; set; }
        public required string Label { get; set; }
        public required string Type { get; set; }
        public bool Nillable { get; set; }
        public bool Createable { get; set; }
        public bool Updateable { get; set; }
        public bool ExternalId { get; set; }
        public bool Custom { get; set; }
        public List<string> ReferenceTo { get; set; } = new();
        public string? RelationshipName { get; set; }
    }
}