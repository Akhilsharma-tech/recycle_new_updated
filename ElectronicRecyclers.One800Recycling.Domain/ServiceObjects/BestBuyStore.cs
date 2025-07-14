using System.Text.Json.Serialization;

namespace ElectronicRecyclers.One800Recycling.Domain.ServiceObjects
{
    public class BestBuyStore : IServiceEnvironmentalOrganization
    {
        [JsonPropertyName("longName")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("address2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("region")]
        public string State { get; set; }

        [JsonIgnore]
        public string Region { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lng")]
        public double Longitude { get; set; }

        [JsonPropertyName("phone")]
        public string Telephone { get; set; }

        [JsonIgnore]
        public string Fax { get; set; }

        [JsonIgnore]
        public string WebsiteUrl { get; set; }

        [JsonPropertyName("hours")]
        public string HoursOfOperation { get; set; }
    }
}
