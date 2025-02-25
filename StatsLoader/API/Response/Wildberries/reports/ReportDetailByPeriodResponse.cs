using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries.reports
{
    internal class ReportDetailByPeriodResponse : IWildberriesResponse
    {
        public decimal Id { get; set; }
        public decimal realizationreport_id {get;set;}
        public string date_from {get;set;} 
        public string date_to {get;set;}
        public string create_dt {get;set;} 
        public string currency_name {get;set;} 
        public object suppliercontract_code {get;set;} 
        public decimal rrd_id {get;set;} 
        public decimal gi_id {get;set;} 
        public decimal dlv_prc {get;set;} 
        public string fix_tariff_date_from {get;set;} 
        public string fix_tariff_date_to {get;set;}
        public string subject_name {get;set;} 
        public decimal nm_id {get;set;} 
        public string brand_name {get;set;} 
        public string sa_name {get;set;}
        public string ts_name {get;set;}
        public string barcode {get;set;} 
        public string doc_type_name {get;set;} 
        public decimal quantity {get;set;} 
        public decimal retail_price {get;set;}
        public decimal retail_amount {get;set;} 
        public decimal sale_percent {get;set;} 
        public decimal commission_percent {get;set;}
        public string office_name {get;set;}
        public string supplier_oper_name {get;set;}
        public string order_dt {get;set;}
        public string sale_dt {get;set;} 
        public string rr_dt {get;set;}
        public decimal shk_id {get;set;}
        public decimal retail_price_withdisc_rub {get;set;}
        public decimal delivery_amount {get;set;} 
        public decimal return_amount {get;set;} 
        public decimal delivery_rub {get;set;} 
        public string gi_box_type_name {get;set;}
        public decimal product_discount_for_report {get;set;}
        public decimal supplier_promo {get;set;} 
        public decimal rid {get;set;}
        public decimal ppvz_spp_prc {get;set;}
        public decimal ppvz_kvw_prc_base {get;set;}
        public decimal ppvz_kvw_prc {get;set;}
        public string sup_rating_prc_up {get;set;}
        public string is_kgvp_v2 {get;set;}
        public decimal ppvz_sales_commission {get;set;} 
        public decimal ppvz_for_pay {get;set;}
        public decimal ppvz_reward {get;set;}
        public decimal acquiring_fee {get;set;} 
        public decimal acquiring_percent {get;set;} 
        public string payment_processing {get;set;} 
        public string acquiring_bank {get;set;}
        public decimal ppvz_vw {get;set;} 
        public decimal ppvz_vw_nds {get;set;} 
        public string ppvz_office_name {get;set;} 
        public decimal ppvz_office_id {get;set;} 
        public decimal ppvz_supplier_id {get;set;} 
        public string ppvz_supplier_name {get;set;} 
        public string ppvz_inn {get;set;} 
        public string declaration_number {get;set;} 
        public string bonus_type_name {get;set;}
        public string sticker_id {get;set;}
        public string site_country {get;set;}
        public bool srv_dbs {get;set;} 
        public decimal penalty {get;set;}
        public decimal additional_payment {get;set;} 
        public decimal rebill_logistic_cost {get;set;}
        public string rebill_logistic_org {get;set;}
        public decimal storage_fee {get;set;}
        public decimal deduction {get;set;}
        public decimal acceptance {get;set;}
        public decimal assembly_id {get;set;}
        public string kiz {get;set;} 
        public string srid {get;set;}
        public decimal report_type {get;set;}
        public bool is_legal_entity {get;set;}
        public string trbx_id { get; set; } 
    }
}
