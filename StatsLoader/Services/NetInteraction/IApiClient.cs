using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatsLoader.Services.NetInteraction
{
    public interface IApiClient
    {
        public Task<string> GetDataAsync(string endpoint, Dictionary<string, string>? queryParams = null);
        public Task<string> PostDataAsync(string endpoint, object body);
    }
}
