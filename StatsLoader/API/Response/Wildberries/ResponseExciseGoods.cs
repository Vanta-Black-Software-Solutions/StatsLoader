using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class ResponseExciseGoods : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("excise_stamp")]
        public string ExciseStamp { get; set; }

        [JsonPropertyName("warehouse_name")]
        public string WarehouseName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
