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

        [JsonIgnore]  public int Id { get; set; }
        public string date { get; set;}
        public string lastChangeDate { get; set; }
        public string warehouseName { get; set;}
        public string warehouseType { get; set;}
        public string countryName { get; set;}
        public string oblastOkrugName { get; set;}
        public string regionName { get; set;}
        public string supplierArticle { get; set;}
        public int nmId { get; set;}
        public string barcode { get; set;}
        public string category { get; set;}
        public string subject { get; set;}
        public string brand { get; set;}
        public string techSize { get; set;}
        public int incomeID { get; set;}
        public bool isSupply { get; set;}
        public bool isRealization { get; set;}
        public int totalPrice { get; set;}
        public int discountPercent { get; set;}
        public int spp { get; set;}
        public int paymentSaleAmount { get; set;}
        public decimal forPay { get; set;}
        public decimal finishedPrice { get; set;}
        public decimal priceWithDisc { get; set;}
        public string saleID { get; set;}
        public string orderType { get; set;}
        public string sticker { get; set;}
        public string gNumber { get; set;}
        public string srid { get; set;}

    }
}
