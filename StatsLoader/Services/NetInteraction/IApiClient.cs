using StatsLoader.API.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace StatsLoader.Services.NetInteraction
{
    public interface IApiClient
    {
        /// <summary>
        /// Асинхронно выполняет GET-запрос к указанному URL.
        /// </summary>
        /// <param name="endpoint">URL, к которому отправляется запрос.</param>
        /// <param name="queryParams">Дополнительные параметры запроса (Query Parameters).</param>
        public Task<string> GetDataAsync(string endpoint, Dictionary<string, string>? queryParams = null);

        /// <summary>
        /// Ассинхронно выполняет POST-запрос к указанному URL.
        /// </summary>
        /// <param name="endpoint">URL, к которому отправляется запрос.</param>
        /// <param name="body">Тело запроса (body).</param>
        /// <returns>Ответ от сервера в виде строки.</returns>
        public Task<string> PostDataAsync(string endpoint, object body);

        /// <summary>
        /// Запрашивает полный отчет от API клиента.
        /// </summary>
        /// <param name="request">Запрос к API.</param>
        /// <returns>Успех выполнения задачи.</returns>
        public Task<bool> GetReport(BaseRequest request);
    }
}
