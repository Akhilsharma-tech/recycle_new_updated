using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class SmartyStreetsResponseJson
    {
        [JsonProperty("input_id")]
        public string Id { get; set; }

        [JsonProperty("delivery_line_1")]
        public string DeliveryAddressLine1 { get; set; }

        [JsonProperty("delivery_line_2")]
        public string DeliveryAddressLine2 { get; set; }

        [JsonProperty("last_line")]
        public string DeliveryCityStateZip { get; set; }

        [JsonProperty("delivery_point_barcode")]
        public string DeliveryPointBarcode { get; set; }

        [JsonProperty("components")]
        public AddressDetails Address { get; set; }

        [JsonProperty("metadata")]
        public AddressGeocodingDetails AddressGeocoding { get; set; }

        public class AddressDetails
        {
            [JsonProperty("primary_number")]
            public string PrimaryNumber { get; set; }

            [JsonProperty("street_predirection")]
            public string StreetPredirection { get; set; }

            [JsonProperty("street_name")]
            public string StreetName { get; set; }

            [JsonProperty("street_suffix")]
            public string StreetSuffix { get; set; }

            [JsonProperty("secondary_designator")]
            public string StreetSuiteOrOfficeInicator { get; set; }

            [JsonProperty("secondary_number")]
            public string StreetSuiteOrOfficeNumber { get; set; }

            [JsonProperty("city_name")]
            public string City { get; set; }

            [JsonProperty("state_abbreviation")]
            public string State { get; set; }

            [JsonProperty("zipcode")]
            public string Zip { get; set; }

            [JsonProperty("plus4_code")]
            public string ZipPlusFour { get; set; }
        }

        public class AddressGeocodingDetails
        {
            [JsonProperty("county_name")]
            public string CountyName { get; set; }

            [JsonProperty("latitude")]
            public string Latitude { get; set; }

            [JsonProperty("longitude")]
            public string Longitude { get; set; }
        }
    }
}
