using System;
using System.Collections.Generic;

namespace StatsLoader.API.Request.Wildberries
{
        // Общий базовый класс для запросов, которым нужен только параметр dateFrom
    public abstract class DateFromOnlyRequest : BaseRequest
    {
        public override Dictionary<string, string> ToQueryParams()
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams["dateFrom"] = dateFrom.Value.ToString("yyyy-MM-dd");
            return queryParams;
        }
    }

    public class IncomesRequest : DateFromOnlyRequest { }
    public class StockRequest : DateFromOnlyRequest { }
    public class OrdersRequest : DateFromOnlyRequest { }
    public class SalesRequest : DateFromOnlyRequest { }

    public class ReportDetailByPeriodRequest : BaseRequest
    {
        public override Dictionary<string, string> ToQueryParams()
        {
            return base.ToQueryParams();
        }
    }
}

