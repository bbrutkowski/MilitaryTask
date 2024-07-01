using System.Text.Json.Serialization;

namespace MilitaryTask.Model.DTO
{
    public class BillEntryDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("occurredAt")]
        public DateTime OccurredAt { get; set; }

        [JsonPropertyName("type")]
        public BillTypeDto Type { get; set; }

        [JsonPropertyName("offer")]
        public OfferDto Offer { get; set; }

        [JsonPropertyName("value")]
        public ValueDto Value { get; set; }

        [JsonPropertyName("tax")]
        public TaxRateDto Tax { get; set; }

        [JsonPropertyName("balance")]
        public AccountBalanceDto Balance { get; set; }
    }

    public class BillingEntriesListDto
    {
        [JsonPropertyName("billingEntries")]
        public List<BillEntryDto> BillingEntries { get; set; }
    }
}
