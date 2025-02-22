using System;
using System.Collections.Generic;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.API.Response.Wildberries;
using StatsLoader.API.Response.Wildberries.DeserializableStruct;

namespace StatsLoader.API.Endpoints
{
    internal class WildberriesApiEndpoints
    {
        public Dictionary<string, (string endpointUrl, Type responseClass, Type requestClass,  bool isPost)> wbEndpoints { get; private set; }

        public WildberriesApiEndpoints()
        {
            wbEndpoints = new Dictionary<string, (string, Type, Type, bool)>
            {
                // Отчёты – используем версию v5; reportDetailByPeriod требует limit
                { "reportDetailByPeriod", ("https://statistics-api.wildberries.ru/api/v5/supplier/reportDetailByPeriod", typeof(ResponseReportDetailByPeriod), typeof(ReportDetailByPeriodRequest), false) },
                { "sales", ("https://statistics-api.wildberries.ru/api/v1/supplier/sales", typeof(ResponseSales), typeof(ReportDetailByPeriodRequest), false) },
                { "stocks", ("https://statistics-api.wildberries.ru/api/v1/supplier/stocks", typeof(ResponseStocks), typeof(ReportDetailByPeriodRequest), false) },
                { "incomes", ("https://statistics-api.wildberries.ru/api/v1/supplier/incomes", typeof(ResponseIncomes), typeof(ReportDetailByPeriodRequest), false) },
                { "orders", ("https://statistics-api.wildberries.ru/api/v1/supplier/orders", typeof(ResponseOrders), typeof(ReportDetailByPeriodRequest), false) },   
               /* { "sales", ("https://statistics-api.wildberries.ru/api/v1/supplier/sales", typeof(DateFromOnlyRequest), typeof(ReportDetailByPeriodRequest), false) },
                { "stocks", ("https://statistics-api.wildberries.ru/api/v1/supplier/stocks", typeof(DateFromOnlyRequest), typeof(ReportDetailByPeriodRequest), false) },
                { "incomes", ("https://statistics-api.wildberries.ru/api/v1/supplier/incomes", typeof(DateFromOnlyRequest), typeof(ReportDetailByPeriodRequest), false) },
                { "orders", ("https://statistics-api.wildberries.ru/api/v1/supplier/orders", typeof(DateFromOnlyRequest), typeof(ReportDetailByPeriodRequest), false) }, */
                //{ "excise_goods", ("https://statistics-api.wildberries.ru/api/v1/supplier/excise-goods", typeof(ResponseExciseGoods), typeof(ReportDetailByPeriodRequest), false) }
                
                /*,
                                         
                // Аналитика – используем версию v1, лимит не передаём
                                          /*
                { "analytics_supplier", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics", typeof(ResponseSupplierAnalytics), false) },
                { "analytics_supplier_bestsellers", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/bestsellers", typeof(ResponseBestSellers), false) },
                { "analytics_supplier_demand", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/demand", typeof(ResponseDemandAnalytics), false) },
                { "analytics_supplier_financials", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/financials", typeof(ResponseFinancialAnalytics), false) },
                { "analytics_supplier_ratings", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/ratings", typeof(ResponseRatingsAnalytics), false) },
                { "analytics_supplier_returns", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/returns", typeof(ResponseReturnsAnalytics), false) },
                { "analytics_supplier_turnover", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/turnover", typeof(ResponseTurnoverAnalytics), false) },
                { "analytics_supplier_geo", ("https://statistics-api.wildberries.ru/api/v1/supplier/analytics/geo", typeof(ResponseGeoAnalytics), false) }    
                                          */
            };

        }
    }
}
