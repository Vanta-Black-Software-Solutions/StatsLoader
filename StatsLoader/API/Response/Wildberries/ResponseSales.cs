using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseSales : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("warehouse_name")]
        public string WarehouseName { get; set; }

        [JsonPropertyName("sale_count")]
        public int SaleCount { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }
}
