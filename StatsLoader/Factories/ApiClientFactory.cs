using StatsLoader.API.Request.Wildberries;
using StatsLoader.Services.NetInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static StatsLoader.AppConfig;

namespace StatsLoader.Factories
{
    public static class ApiClientFactory
    {
        public static IApiClient Create(AppConfig.ApiPlatform platform, string apiKey)
        {
            return platform switch
            {
                AppConfig.ApiPlatform.Wildberries => new WildberriesApiClient(apiKey),
                // AppConfig.ApiPlatform.Ozon => new OzonApiClient(apiKey),
                // AppConfig.ApiPlatform.Yandex => new YandexApiClient(apiKey),
                _ => throw new ArgumentException("Unknown Platform")
            };
        }

    }
}
