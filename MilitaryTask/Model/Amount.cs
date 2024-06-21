using System.Text.Json.Serialization;

namespace MilitaryTask.Model
{
    public class Amount
    {
        public int Id { get; set; }

        [JsonPropertyName("amount")]
        public string Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
