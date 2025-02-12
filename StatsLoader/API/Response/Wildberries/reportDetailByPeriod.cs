using System;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Response.Wildberries
{
    public class reportDetailByPeriod : IWildberriesResponse
    {
        public int RealizationReportId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime CreateDt { get; set; }
        public string CurrencyName { get; set; }
        public string SupplierContractCode { get; set; }
        public int RrdId { get; set; }
        public int GiId { get; set; }
        public decimal DlvPrc { get; set; }
        public DateTime? FixTariffDateFrom { get; set; }
        public DateTime? FixTariffDateTo { get; set; }
        public string SubjectName { get; set; }
        public int NmId { get; set; }
        public string BrandName { get; set; }
        public string SaName { get; set; }
        public string TsName { get; set; }
        public string Barcode { get; set; }
        public string DocTypeName { get; set; }
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal RetailAmount { get; set; }
        public int SalePercent { get; set; }
        public decimal CommissionPercent { get; set; }
        public string OfficeName { get; set; }
        public string SupplierOperName { get; set; }
        public DateTime OrderDt { get; set; }
        public DateTime SaleDt { get; set; }
        public DateTime RrDt { get; set; }
        public long ShkId { get; set; }
        public decimal RetailPriceWithDiscRub { get; set; }
        public int DeliveryAmount { get; set; }
        public int ReturnAmount { get; set; }
        public decimal DeliveryRub { get; set; }
        public string GiBoxTypeName { get; set; }
        public decimal ProductDiscountForReport { get; set; }
        public decimal SupplierPromo { get; set; }
        public int Rid { get; set; }
        public decimal PpvzSppPrc { get; set; }
        public decimal PpvzKvwPrcBase { get; set; }
        public decimal PpvzKvwPrc { get; set; }
        public decimal SupRatingPrcUp { get; set; }
        public decimal IsKgvpV2 { get; set; }
        public decimal PpvzSalesCommission { get; set; }
        public decimal PpvzForPay { get; set; }
        public decimal PpvzReward { get; set; }
        public decimal AcquiringFee { get; set; }
        public decimal AcquiringPercent { get; set; }
        public string PaymentProcessing { get; set; }
        public string AcquiringBank { get; set; }
        public decimal PpvzVw { get; set; }
        public decimal PpvzVwNds { get; set; }
        public string PpvzOfficeName { get; set; }
        public int PpvzOfficeId { get; set; }
        public int PpvzSupplierId { get; set; }
        public string PpvzSupplierName { get; set; }
        public string PpvzInn { get; set; }
        public string DeclarationNumber { get; set; }
        public string BonusTypeName { get; set; }
        public string StickerId { get; set; }
        public string SiteCountry { get; set; }
        public bool SrvDbs { get; set; }
        public decimal Penalty { get; set; }
        public decimal AdditionalPayment { get; set; }
        public decimal RebillLogisticCost { get; set; }
        public string RebillLogisticOrg { get; set; }
        public decimal StorageFee { get; set; }
        public decimal Deduction { get; set; }
        public decimal Acceptance { get; set; }
        public int AssemblyId { get; set; }
        public string Kiz { get; set; }
        public string Srid { get; set; }
        public int ReportType { get; set; }
        public bool IsLegalEntity { get; set; }
        public string TrbxId { get; set; }
    }
}
