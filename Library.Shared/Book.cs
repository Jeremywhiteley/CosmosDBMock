// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Library.Shared
{
    public class Book
    {
        // CosmosDB uses Newtonsoft.Json for serialize, when Api REST uses System.Text.Json
        [JsonProperty("id")]
        [JsonPropertyName("isbn")]
        public string ISBN { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("image_link")]
        public string ImageLink { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        // ** PartitionKey: automated setting the server country 
        public string ServiceCountry { get; set; }

        public override string ToString() => $"{Title}, {Author}";
    }
}
