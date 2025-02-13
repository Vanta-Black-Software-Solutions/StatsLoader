using StatsLoader.API.Request.Wildberries;
using StatsLoader.Services.NetInteraction;
using System;
using static StatsLoader.AppConfig;

namespace StatsLoader.Factories
{
    /// <summary>
    /// Фабрика для создания API-клиента в зависимости от платформы.
    /// </summary>
    public static class ApiClientFactory
    {
        public static IApiClient Create(ApiPlatform platform, string apiKey)
        {
            return platform switch
            {
                ApiPlatform.Wildberries => new WildberriesApiClient(apiKey),
                // ApiPlatform.Ozon => new OzonApiClient(apiKey),
                // ApiPlatform.Yandex => new YandexApiClient(apiKey),
                _ => throw new ArgumentException("Unknown Platform")
            };
        }
    }
}
