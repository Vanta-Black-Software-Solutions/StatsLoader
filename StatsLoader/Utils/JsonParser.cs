using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StatsLoader.API.Response.Wildberries;
    

namespace StatsLoader.Utils
{
    internal class JsonParser
    {
        public static List<T> ParseResponse<T>(string jsonData) where T : IWildberriesResponse
        {
            try
            {
                return JsonSerializer.Deserialize<List<T>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<T>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<T>();
            }
        }
    }
}
