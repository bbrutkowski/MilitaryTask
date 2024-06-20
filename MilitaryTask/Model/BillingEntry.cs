namespace MilitaryTask.Model
{
    public class BillingEntry
    {
        public string Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public BillingEntryType Type { get; set; }
        public Offer Offer { get; set; }
        public Money Value { get; set; }
        public Tax Tax { get; set; }
        public Money Balance { get; set; }
    }

    public class BillingEntriesResponse
    {
        public List<BillingEntry> BillingEntries { get; set; }
    }
}
