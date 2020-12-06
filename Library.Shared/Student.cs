// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Newtonsoft.Json;
using System;
// using J = System.Text.Json.Serialization;

namespace Library.Shared
{
    public class Student
    {
        // CosmosDB requires id in container 
        [JsonProperty("id")]
        public string Identifier { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthdate { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        // ** PartitionKey: automated setting the server country 
        public string Partition { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} | {Identifier} | {Birthdate:dd-MM-yyyy}";
        }
    }
}

