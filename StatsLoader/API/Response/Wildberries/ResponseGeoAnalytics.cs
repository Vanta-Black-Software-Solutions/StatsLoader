using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseGeoAnalytics : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("sales_count")]
        public int SalesCount { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }
}
