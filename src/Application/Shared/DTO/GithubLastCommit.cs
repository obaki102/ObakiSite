using System.Text.Json.Serialization;

namespace ObakiSite.Application.Shared.DTO
{
    public class Object
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class GithubLastCommit
    {
        [JsonPropertyName("ref")]
        public string Ref { get; set; } = string.Empty;

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("object")]
        public Object Object { get; set; } = new();
    }
}
