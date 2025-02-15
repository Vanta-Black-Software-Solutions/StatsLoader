using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseFinancialAnalytics : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("supplier_id")]
        public int SupplierId { get; set; }

        [JsonPropertyName("total_revenue")]
        public decimal TotalRevenue { get; set; }

        [JsonPropertyName("total_expense")]
        public decimal TotalExpense { get; set; }

        [JsonPropertyName("net_profit")]
        public decimal NetProfit { get; set; }
    }
}
