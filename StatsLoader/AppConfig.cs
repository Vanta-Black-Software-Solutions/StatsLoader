using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLoader
{
    public static class AppConfig
    {
        private static ConnectionString connStr = new ConnectionString();


        public static string connectionString = connStr.ConnString;
        public static string WbApiKey = "eyJhbGciOiJFUzI1NiIsImtpZCI6IjIwMjUwMTIwdjEiLCJ0eXAiOiJKV1QifQ.eyJlbnQiOjEsImV4cCI6MTc1NDkwMDY4MCwiaWQiOiIwMTk0ZWM2Mi1iYWZhLTc5MDEtYWY1MC0zZGYxN2NjNGZhZDMiLCJpaWQiOjMxOTQyMjIyLCJvaWQiOjUxNTIxLCJzIjoxMDczNzQxODYwLCJzaWQiOiI1MTZlYTNjMC0xMjY0LTU1MmYtOGQ0OC0yMzRiZTY2YWY4MTIiLCJ0IjpmYWxzZSwidWlkIjozMTk0MjIyMn0.bd8OFcqbZ8jOPLmdgQTPVFYSDgLhcAhWHHrggLLLZu-dWe2zNBeOv3BatoYXG3lNRTL9Cuk8dj8ieoKR2fkYWA";


        public enum ApiPlatform
        {
            Wildberries,
            Ozon
        }


    }

    public class ConnectionString
    {

        public string HOST { get; private set; }
        public string DB { get; private set; }
        public string USERNAME { get; private set; }
        public string PASSWORD { get; private set; }
        public string ConnString { get; private set; }

        public ConnectionString()
        {
            HOST = "ep-winter-mouse-a9l6j2be-pooler.gwc.azure.neon.tech";
            DB = "neondb";
            USERNAME = "neondb_owner";
            PASSWORD = "npg_kA4pbgFl0tyY";

            ConnString = $"Host={HOST};Database={DB};Username={USERNAME};Password={PASSWORD}";
        }
    }
}
