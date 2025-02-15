using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseStocks : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("warehouse_name")]
        public string WarehouseName { get; set; }

        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }

        [JsonPropertyName("stock_count")]
        public int StockCount { get; set; }
    }
}
