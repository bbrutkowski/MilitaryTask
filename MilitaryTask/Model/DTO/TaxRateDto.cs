using System.Text.Json.Serialization;

namespace MilitaryTask.Model.DTO
{
    public class TaxRateDto
    {
        [JsonPropertyName("percentage")]
        public string Percentage { get; set; }
    }
}
