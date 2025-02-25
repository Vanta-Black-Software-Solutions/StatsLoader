using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries.reports
{
    internal class StocksResponse : IWildberriesResponse
    {
        public int Id { get; set; }
        public string lastChangeDate {get; set;}
        public string warehouseName { get; set; }
        public string supplierArticle {get; set;} 
        public int nmId {get; set;}
        public string barcode {get; set;} 
        public int quantity {get; set;}
        public int inWayToClient {get; set;} 
        public int inWayFromClient {get; set;}
        public int quantityFull {get; set;} 
        public string category {get; set;}
        public string subject {get; set;}
        public string brand {get; set;}
        public string techSize {get; set;} 
        public int Price {get; set;} 
        public int Discount {get; set;}
        public bool isSupply {get; set;}
        public bool isRealization {get; set;} 
        public string SCCode { get; set; } 
    }
}
