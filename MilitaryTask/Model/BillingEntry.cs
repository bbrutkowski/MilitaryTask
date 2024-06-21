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
        public Type Type { get; set; }

        [JsonPropertyName("offer")]
        public Offer Offer { get; set; }

        [JsonPropertyName("value")]
        public Amount Value { get; set; }

        [JsonPropertyName("tax")]
        public Tax Tax { get; set; }

        [JsonPropertyName("balance")]
        public Balance Balance { get; set; }
    }

    public class BillingEntriesList
    {
        [JsonPropertyName("billingEntries")]
        public IReadOnlyCollection<BillingEntry> BillingEntries { get; set; }
    }
}
