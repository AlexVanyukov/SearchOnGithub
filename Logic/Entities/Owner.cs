using System.Text.Json.Serialization;

namespace Logic.Entities
{
    public class Owner
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
    }
}
