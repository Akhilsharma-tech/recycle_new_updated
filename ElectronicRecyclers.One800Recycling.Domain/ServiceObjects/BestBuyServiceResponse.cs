using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ElectronicRecyclers.One800Recycling.Domain.ServiceObjects
{
    public class BestBuyServiceResponse
    {
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("stores")]
        public IEnumerable<BestBuyStore> Stores { get; set; } 
    }
}