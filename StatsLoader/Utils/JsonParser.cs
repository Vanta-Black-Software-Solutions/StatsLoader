using System;
using System.Text.Json;

namespace StatsLoader.Utils
{
    internal class JsonParser
    {
        public static object ParseResponse(string jsonData, Type responseType)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                // Десериализуем в List<T>
                return JsonSerializer.Deserialize(jsonData, typeof(System.Collections.Generic.List<>).MakeGenericType(responseType), options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON Parse Error: {ex.Message}");
                // Возвращаем пустой список нужного типа, если произошла ошибка
                return Activator.CreateInstance(typeof(System.Collections.Generic.List<>).MakeGenericType(responseType));
            }
        }
    }
}
