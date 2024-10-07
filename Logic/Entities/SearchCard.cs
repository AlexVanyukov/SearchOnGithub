using System.Text.Json.Serialization;

namespace Logic.Entities
{
    public class SearchCards : List<SearchCard>
    {
        public List<SearchCard> Cards { get; set; }
    }

    public class SearchCard
    {
        [JsonPropertyName("name_project")]
        public string NameProject { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonPropertyName("watchers_count")]
        public int WatchersCount { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
