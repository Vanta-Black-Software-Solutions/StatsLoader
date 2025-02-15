using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseRatingsAnalytics : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("supplier_id")]
        public int SupplierId { get; set; }

        [JsonPropertyName("rating_score")]
        public decimal RatingScore { get; set; }

        [JsonPropertyName("review_count")]
        public int ReviewCount { get; set; }
    }
}
