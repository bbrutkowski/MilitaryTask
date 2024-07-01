using System.Text.Json.Serialization;

namespace MilitaryTask.Model.DTO
{
    public class OfferDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
