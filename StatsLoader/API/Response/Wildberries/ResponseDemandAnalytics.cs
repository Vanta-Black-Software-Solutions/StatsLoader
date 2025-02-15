using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseDemandAnalytics : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("demand_index")]
        public decimal DemandIndex { get; set; }

        [JsonPropertyName("sales_count")]
        public int SalesCount { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }
}
