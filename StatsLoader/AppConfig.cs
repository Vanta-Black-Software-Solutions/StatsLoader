using System;
using System.Collections.Generic;
using System.IO;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.API.Request;

namespace StatsLoader
{
    public static class AppConfig
    {
        // API ключи
        public static string WbApiKey { get; } = EnvLoader.Get("WB_API_KEY", "eyJhbGciOiJFUzI1NiIsImtpZCI6IjIwMjUwMTIwdjEiLCJ0eXAiOiJKV1QifQ.eyJlbnQiOjEsImV4cCI6MTc1NDkwMDY4MCwiaWQiOiIwMTk0ZWM2Mi1iYWZhLTc5MDEtYWY1MC0zZGYxN2NjNGZhZDMiLCJpaWQiOjMxOTQyMjIyLCJvaWQiOjUxNTIxLCJzIjoxMDczNzQxODYwLCJzaWQiOiI1MTZlYTNjMC0xMjY0LTU1MmYtOGQ0OC0yMzRiZTY2YWY4MTIiLCJ0IjpmYWxzZSwidWlkIjozMTk0MjIyMn0.bd8OFcqbZ8jOPLmdgQTPVFYSDgLhcAhWHHrggLLLZu-dWe2zNBeOv3BatoYXG3lNRTL9Cuk8dj8ieoKR2fkYWA");

        // Подключение к БД
        public static string ConnectionString { get; } = new ConnectionString().ConnString;

        // Дефолтное количество дней для запросов (-7 дней)
        private static readonly int CountDay = -7;

        // Дефолтные параметры запроса для Wildberries
        public static BaseRequest DefaultWildberriesRequestData => new RequestReportDetailByPeriod
        {
            dateFrom = DateTime.UtcNow.AddDays(CountDay),
            dateTo = DateTime.UtcNow,
            Limit = 1000
            
        };

        // Маркетплейсы
        public enum ApiPlatform
        {
            Wildberries,
            Ozon,
            Yandex
        }
    }

    public class ConnectionString
    {
        public string Host { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }
        public string ConnString { get; }

        public ConnectionString()
        {
            Host = EnvLoader.Get("DB_HOST", "ep-winter-mouse-a9l6j2be-pooler.gwc.azure.neon.tech");
            Database = EnvLoader.Get("DB_NAME", "neondb");
            Username = EnvLoader.Get("DB_USER", "neondb_owner");
            Password = EnvLoader.Get("DB_PASS", "npg_kA4pbgFl0tyY");

            ConnString = $"Host={Host};Database={Database};Username={Username};Password={Password};";
        }
    }

    public static class EnvLoader
    {
        private static readonly Dictionary<string, string> EnvVariables = new Dictionary<string, string>();

        static EnvLoader()
        {
            if (!File.Exists(".env")) return;
            foreach (var line in File.ReadAllLines(".env"))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    EnvVariables[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        public static string Get(string key, string defaultValue = "")
        {
            return EnvVariables.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }

}
