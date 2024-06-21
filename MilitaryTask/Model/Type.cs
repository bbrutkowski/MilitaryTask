using System.Text.Json.Serialization;

namespace MilitaryTask.Model
{
    public class Type
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
