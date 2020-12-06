﻿// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Newtonsoft.Json;
using J = System.Text.Json.Serialization;

namespace Library.Shared
{
    public class Book
    {
        // CosmosDB uses Newtonsoft.Json for serialize, when Api REST uses System.Text.Json
        [JsonProperty("id")]
        public string ISBN { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("image_link")]
        public string ImageLink { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [J.JsonIgnore]
        // ** PartitionKey: automated setting the server country 
        public string ServiceCountry { get; set; }

        public override string ToString() => $"{Title}, {Author}";
    }
}
