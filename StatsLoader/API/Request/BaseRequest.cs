using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace StatsLoader.API.Request
{
    public abstract class BaseRequest
    {
        [JsonIgnore] public bool IsPost;

        public DateTime? dateFrom {  get; set; }
        public DateTime? dateTo { get; set; }
        public int? Limit { get; set; }
        public string SupplierArticle { get; set; }
        public string Barcode { get; set; }

        public virtual Dictionary<string, string> ToQueryParams()
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            queryParams["dateFrom"] = dateFrom?.ToString("yyyy-MM-dd") ?? "";
            queryParams["dateTo"] = dateTo?.ToString("yyyy-MM-dd") ?? "";
            if (Limit.HasValue) queryParams.Add("limit", Limit.Value.ToString());
            if (!string.IsNullOrWhiteSpace(SupplierArticle)) queryParams.Add("supplierArticle", SupplierArticle);
            if (!string.IsNullOrWhiteSpace(Barcode)) queryParams.Add("barcode", Barcode);

            return queryParams;
        }

        public virtual string ToJsonBody()
        {
            return JsonSerializer.Serialize(this);
        }

        public static implicit operator BaseRequest(string v)
        {
            throw new NotImplementedException();
        }
    }
}
