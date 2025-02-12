using System.Collections.Generic;

namespace StatsLoader.API.Request.Wildberries
{
    internal static class WbApiEndpoints
    {
        public static readonly Dictionary<string, (string FullUrl, bool IsPost)> ApiEndpoints = new()
        {
            { "sales_reports", ("https://statistics-api.wildberries.ru/api/v5/supplier/reportDetailByPeriod", false) }
        };
    }

}
