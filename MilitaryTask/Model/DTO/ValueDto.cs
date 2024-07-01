using System.Text.Json.Serialization;

namespace MilitaryTask.Model.DTO
{
    public class ValueDto
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
