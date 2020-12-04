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
        // nice for primary key
        [JsonPropertyName("country_id")]
        public int CountryId { get; set; }
        [JsonIgnore]
        // ** PartitionKey: automated setting the server country 
        public string ServiceCountry { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} | {Identifier}";
        }
    }
}
