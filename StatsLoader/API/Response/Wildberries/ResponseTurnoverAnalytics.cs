using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseTurnoverAnalytics : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("turnover")]
        public decimal Turnover { get; set; }
    }
}
