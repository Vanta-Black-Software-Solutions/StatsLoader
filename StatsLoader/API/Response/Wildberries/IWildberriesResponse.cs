using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries
{
    public interface IWildberriesResponse
    {
        [JsonIgnore]
        public int Id { get; set; } // Уникальный идентификатор для хранения в БД (если нужен)
    }
}
