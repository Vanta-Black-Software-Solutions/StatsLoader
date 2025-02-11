using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLoader
{
    internal class AppConfig
    {


        public enum ApiPlatform
        {
            Wildberries
        }


    }

    internal class ConnectionString
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
