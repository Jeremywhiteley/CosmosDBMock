// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace Library.Shared
{
    public class Student
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("birthday")]
        public DateTime Birthdate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        // ** PartitionKey: automated setting the server country 
        public string ServiceCountry { get; set; }

        // CosmosDB uses Newtonsoft.Json for serialize, when Api REST uses System.Text.Json
        // and id has to be string.
        [JsonProperty("id")]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Key {
            get { return Id.ToString(); }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} | {Identifier}";
        }
    }
}
