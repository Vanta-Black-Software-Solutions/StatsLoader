using System;
using System.Collections.Generic;
using System.Text.Json;

namespace StatsLoader.Utils
{
    internal class JsonParser
    {

        public static List<T> ParseResponse<T>(string jsonData) where T : class
        {
            try
            {
                return JsonSerializer.Deserialize<List<T>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON Parse Error: {ex.Message}", Console.ForegroundColor = ConsoleColor.Red);
                return new List<T>();
            }
        }


    }
}
