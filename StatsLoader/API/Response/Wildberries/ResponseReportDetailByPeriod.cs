using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries.DeserializableStruct
{
    public class ResponseReportDetailByPeriod : IWildberriesResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("realizationreport_id")]
        public int RealizationReportId { get; set; }

        [JsonPropertyName("date_from")]
        public DateTime DateFrom { get; set; }

        [JsonPropertyName("date_to")]
        public DateTime DateTo { get; set; }

        [JsonPropertyName("create_dt")]
        public DateTime CreateDt { get; set; }

        [JsonPropertyName("currency_name")]
        public string CurrencyName { get; set; }

        [JsonPropertyName("suppliercontract_code")]
        public string SupplierContractCode { get; set; }

        [JsonPropertyName("rrd_id")]
        public long RrdId { get; set; }

        [JsonPropertyName("gi_id")]
        public int GiId { get; set; }

        [JsonPropertyName("dlv_prc")]
        public decimal DlvPrc { get; set; }

        [JsonPropertyName("fix_tariff_date_from")]
        public string FixTariffDateFrom { get; set; }

        [JsonPropertyName("fix_tariff_date_to")]
        public string FixTariffDateTo { get; set; }

        [JsonPropertyName("subject_name")]
        public string SubjectName { get; set; }

        [JsonPropertyName("nm_id")]
        public int NmId { get; set; }

        [JsonPropertyName("brand_name")]
        public string BrandName { get; set; }

        [JsonPropertyName("sa_name")]
        public string SaName { get; set; }

        [JsonPropertyName("ts_name")]
        public string TsName { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }

        [JsonPropertyName("doc_type_name")]
        public string DocTypeName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("retail_price")]
        public decimal RetailPrice { get; set; }

        [JsonPropertyName("retail_amount")]
        public decimal RetailAmount { get; set; }

        [JsonPropertyName("sale_percent")]
        public int SalePercent { get; set; }

        [JsonPropertyName("commission_percent")]
        public decimal CommissionPercent { get; set; }

        [JsonPropertyName("office_name")]
        public string OfficeName { get; set; }

        [JsonPropertyName("supplier_oper_name")]
        public string SupplierOperName { get; set; }

        [JsonPropertyName("order_dt")]
        public DateTime OrderDt { get; set; }

        [JsonPropertyName("sale_dt")]
        public DateTime SaleDt { get; set; }

        [JsonPropertyName("rr_dt")]
        public DateTime RrDt { get; set; }

        [JsonPropertyName("shk_id")]
        public long ShkId { get; set; }

        [JsonPropertyName("retail_price_withdisc_rub")]
        public decimal RetailPriceWithDiscRub { get; set; }

        [JsonPropertyName("delivery_amount")]
        public int DeliveryAmount { get; set; }

        [JsonPropertyName("return_amount")]
        public int ReturnAmount { get; set; }

        [JsonPropertyName("delivery_rub")]
        public decimal DeliveryRub { get; set; }

        [JsonPropertyName("gi_box_type_name")]
        public string GiBoxTypeName { get; set; }

        [JsonPropertyName("product_discount_for_report")]
        public decimal ProductDiscountForReport { get; set; }

        [JsonPropertyName("supplier_promo")]
        public decimal SupplierPromo { get; set; }

        [JsonPropertyName("rid")]
        public int Rid { get; set; }

        [JsonPropertyName("ppvz_spp_prc")]
        public decimal PpvzSppPrc { get; set; }

        [JsonPropertyName("ppvz_kvw_prc_base")]
        public decimal PpvzKvwPrcBase { get; set; }

        [JsonPropertyName("ppvz_kvw_prc")]
        public decimal PpvzKvwPrc { get; set; }

        [JsonPropertyName("sup_rating_prc_up")]
        public decimal SupRatingPrcUp { get; set; }

        [JsonPropertyName("is_kgvp_v2")]
        public decimal IsKgvpV2 { get; set; }

        [JsonPropertyName("ppvz_sales_commission")]
        public decimal PpvzSalesCommission { get; set; }

        [JsonPropertyName("ppvz_for_pay")]
        public decimal PpvzForPay { get; set; }

        [JsonPropertyName("ppvz_reward")]
        public decimal PpvzReward { get; set; }

        [JsonPropertyName("acquiring_fee")]
        public decimal AcquiringFee { get; set; }

        [JsonPropertyName("acquiring_percent")]
        public decimal AcquiringPercent { get; set; }

        [JsonPropertyName("payment_processing")]
        public string PaymentProcessing { get; set; }

        [JsonPropertyName("acquiring_bank")]
        public string AcquiringBank { get; set; }

        [JsonPropertyName("ppvz_vw")]
        public decimal PpvzVw { get; set; }

        [JsonPropertyName("ppvz_vw_nds")]
        public decimal PpvzVwNds { get; set; }

        [JsonPropertyName("ppvz_office_name")]
        public string PpvzOfficeName { get; set; }

        [JsonPropertyName("ppvz_office_id")]
        public int PpvzOfficeId { get; set; }

        [JsonPropertyName("ppvz_supplier_id")]
        public int PpvzSupplierId { get; set; }

        [JsonPropertyName("ppvz_supplier_name")]
        public string PpvzSupplierName { get; set; }

        [JsonPropertyName("ppvz_inn")]
        public string PpvzInn { get; set; }

        [JsonPropertyName("declaration_number")]
        public string DeclarationNumber { get; set; }

        [JsonPropertyName("bonus_type_name")]
        public string BonusTypeName { get; set; }

        [JsonPropertyName("sticker_id")]
        public string StickerId { get; set; }

        [JsonPropertyName("site_country")]
        public string SiteCountry { get; set; }

        [JsonPropertyName("srv_dbs")]
        public bool SrvDbs { get; set; }

        [JsonPropertyName("penalty")]
        public decimal Penalty { get; set; }

        [JsonPropertyName("additional_payment")]
        public decimal AdditionalPayment { get; set; }

        [JsonPropertyName("rebill_logistic_cost")]
        public decimal RebillLogisticCost { get; set; }

        [JsonPropertyName("rebill_logistic_org")]
        public string RebillLogisticOrg { get; set; }

        [JsonPropertyName("storage_fee")]
        public decimal StorageFee { get; set; }

        [JsonPropertyName("deduction")]
        public decimal Deduction { get; set; }

        [JsonPropertyName("acceptance")]
        public decimal Acceptance { get; set; }

        [JsonPropertyName("assembly_id")]
        public int AssemblyId { get; set; }

        [JsonPropertyName("kiz")]
        public string Kiz { get; set; }

        [JsonPropertyName("srid")]
        public string Srid { get; set; }

        [JsonPropertyName("report_type")]
        public int ReportType { get; set; }

        [JsonPropertyName("is_legal_entity")]
        public bool IsLegalEntity { get; set; }

        [JsonPropertyName("trbx_id")]
        public string TrbxId { get; set; }
        
    }
}
