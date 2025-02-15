using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseIncomes : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("income_id")]
        public int IncomeId { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("supplier_id")]
        public int SupplierId { get; set; }

        [JsonPropertyName("total_income")]
        public decimal TotalIncome { get; set; }
    }
}
