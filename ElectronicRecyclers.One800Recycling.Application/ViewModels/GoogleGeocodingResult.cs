using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class GoogleGeocodingResult
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [JsonProperty("geometry")]
        public Geometry AddressGeometry { get; set; }

        public List<String> Types { get; set; }

        public class AddressComponent
        {
            [JsonProperty("long_name")]
            public string LongName { get; set; }

            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("types")]
            public List<string> Types { get; set; }
        }

        public class Location
        {
            [JsonProperty("lat")]
            public Double Latitude { get; set; }

            [JsonProperty("lng")]
            public Double Longitude { get; set; }
        }

        public class Geometry
        {
            [JsonProperty("location")]
            public Location Location { get; set; }

            [JsonProperty("location_type")]
            public string LocationType { get; set; }
        }
    }
}
