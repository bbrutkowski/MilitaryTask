using System.Text.Json.Serialization;

namespace MilitaryTask.Model
{
    public class Tax
    {
        public int Id { get; set; }

        [JsonPropertyName("percentage")]
        public string Percentage { get; set; }
    }
}
