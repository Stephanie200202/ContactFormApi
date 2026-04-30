using System.Text.Json.Serialization;

namespace ContactForm.Dtos
{
    public class RestCountryResponse
    {
        [JsonPropertyName("name")]
        public RestCountryName? Name { get; set; }

        [JsonPropertyName("capital")]
        public List<string>? Capital { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("subregion")]
        public string? Subregion { get; set; }

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("flags")]
        public RestCountryFlags? Flags { get; set; }
    }
    
    public class RestCountryName
    {
        [JsonPropertyName("common")]
        public string? Common { get; set; }
        [JsonPropertyName("official")]
        public string? Official { get; set; }
    }

    public class RestCountryFlags
    {
        [JsonPropertyName("png")]
        public string? Png { get; set; }
        [JsonPropertyName("svg")]
        public string? Svg { get; set; }
    }
}
