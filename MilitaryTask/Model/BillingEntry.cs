using System.Text.Json.Serialization;

namespace MilitaryTask.Model
{
    public class BillingEntry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("occurredAt")]
        public DateTime OccurredAt { get; set; }

        [JsonPropertyName("type")]
        public EntryType Type { get; set; }

        [JsonPropertyName("offer")]
        public Offer Offer { get; set; }

        [JsonPropertyName("value")]
        public Value Value { get; set; }

        [JsonPropertyName("tax")]
        public Tax Tax { get; set; }

        [JsonPropertyName("balance")]
        public Balance Balance { get; set; }
    }

    public class Balance
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
    }

    public class Tax
    {
        public int Id { get; set; }
        [JsonPropertyName("percentage")]
        public string Percentage { get; set; }
    }

    public class Value
    {
        public int Id { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class Offer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class EntryType
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class BillingEntriesList
    {
        [JsonPropertyName("billingEntries")]
        public IReadOnlyCollection<BillingEntry> BillingEntries { get; set; }
    }
}
