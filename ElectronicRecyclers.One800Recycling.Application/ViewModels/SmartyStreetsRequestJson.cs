using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class SmartyStreetsRequestJson
    {
        [JsonProperty("input_id")]
        public string Id { get; set; }

        [JsonProperty("street")]
        public string AddressLine1 { get; set; }

        [JsonProperty("street2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipcode")]
        public string PostalCode { get; set; }
    }
}
