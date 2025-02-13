using System;
using System.Collections.Generic;
using System.IO;
using StatsLoader.API.Request.Wildberries;
using StatsLoader.API.Request;

namespace StatsLoader
{
    public static class AppConfig
    {

        
        private static readonly int CountDay = -7; // DAY COUNT FOR REQUEST




        public static string WbApiKey { get; } = EnvLoader.Get("WB_API_KEY"); // API KEYS
        public static string ConnectionString { get; } = new ConnectionString().ConnString; // CONNECTION TO DB



        // DEFAULT REQUET PARAMS
        public static BaseRequest DefaultWildberriesRequestData => new RequestReportDetailByPeriod
        {
            dateFrom = DateTime.UtcNow.AddDays(CountDay),
            dateTo = DateTime.UtcNow,
            Limit = 1000
            
        };


        // PLATFORM
        public enum ApiPlatform
        {
            Wildberries,
            Ozon,
            Yandex
        }
    }




    // CONNECTION TO DB
    public class ConnectionString
    {
        public string Host { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }
        public string ConnString { get; }

        public ConnectionString()
        {
            Host = EnvLoader.Get("DB_HOST");
            Database = EnvLoader.Get("DB_NAME");
            Username = EnvLoader.Get("DB_USER");
            Password = EnvLoader.Get("DB_PASS");

            ConnString = $"Host={Host};Database={Database};Username={Username};Password={Password};";
        }
    }



    public static class EnvLoader
    {
        private static readonly Dictionary<string, string> EnvVariables = new Dictionary<string, string>();


        public static string Get(string keyPlatfom, string defaultValue = "")
        {
            if (!File.Exists(".env")) return defaultValue;
            else
            {
                string[] lines = File.ReadAllLines(".env");
                foreach (string line in lines)
                {
                    string[] param = line.Split('=');
                    if (param[0] == keyPlatfom) return param[1];
                    else continue;
                }
                return EnvVariables.TryGetValue(keyPlatfom, out var value) ? value : defaultValue;
            }

        }
    }

}
