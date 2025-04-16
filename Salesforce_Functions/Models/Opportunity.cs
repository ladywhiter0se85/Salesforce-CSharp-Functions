namespace Salesforce_Functions
{
        public class Opportunity
        {
                public Attributes Attributes { get; set; } = new Attributes { Type = "Opportunity" };
                public string? Id { get; set; }
                public bool? IsDeleted { get; set; }
                public string? AccountId { get; set; }
                public bool? IsPrivate { get; set; }
                public string? Name { get; set; }
                public string? Description { get; set; }
                public string? StageName { get; set; }
                public double? Amount { get; set; }
                public object? Probability { get; set; }
                public double? ExpectedRevenue { get; set; }
                public double? TotalOpportunityQuantity { get; set; }
                public DateTime? CloseDate { get; set; }
                public string? Type { get; set; }
                public string? NextStep { get; set; }
                public string? LeadSource { get; set; }
                public bool? IsClosed { get; set; }
                public bool? IsWon { get; set; }
                public string? ForecastCategory { get; set; }
                public string? ForecastCategoryName { get; set; }
                public object? CampaignId { get; set; }
                public bool? HasOpportunityLineItem { get; set; }
                public object? Pricebook2Id { get; set; }
                public object? OwnerId { get; set; }
                public DateTime? CreatedDate { get; set; }
                public object? CreatedById { get; set; }
                public DateTime? LastModifiedDate { get; set; }
                public object? LastModifiedById { get; set; }
                public DateTime? SystemModstamp { get; set; }
                public DateTime? LastActivityDate { get; set; }
                public int? PushCount { get; set; }
                public DateTime? LastStageChangeDate { get; set; }
                public int? FiscalQuarter { get; set; }
                public int? FiscalYear { get; set; }
                public string? Fiscal { get; set; }
                public object? ContactId { get; set; }
                public DateTime? LastViewedDate { get; set; }
                public DateTime? LastReferencedDate { get; set; }
                public bool? HasOpenActivity { get; set; }
                public bool? HasOverdueTask { get; set; }
                public object? LastAmountChangedHistoryId { get; set; }
                public object? LastCloseDateChangedHistoryId { get; set; }
                public string? DeliveryInstallationStatus__c { get; set; }
                public string? TrackingNumber__c { get; set; }
                public string? OrderNumber__c { get; set; }
                public string? CurrentGenerators__c { get; set; }
                public string? MainCompetitors__c { get; set; }
        }
}
