using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatsLoader.API.Endpoints;
using StatsLoader.Data;
using StatsLoader.Services.NetInteraction;
using StatsLoader.Utils;

namespace StatsLoader.API.Request.Wildberries
{
    public class WildberriesApiClient : ApiClient
    {
        private DatabaseService databaseService = new DatabaseService();
        private WildberriesApiEndpoints apiEndpoints = new WildberriesApiEndpoints();

        public WildberriesApiClient(string apiKey) : base(apiKey, AppConfig.ApiPlatform.Wildberries) { }


        public override async Task<bool> GetReport(BaseRequest request) 
        {
            try
            {

                Console.WriteLine("Request To API...");
                List<Task> tasks  = new List<Task>();

                foreach (var endpoint in apiEndpoints.wbEndpoints)
                {
                    string tableNameInDatabase = endpoint.Key;
                    string urlApiEndpoint = endpoint.Value.endpointUrl;
                    Type wildberriesResponse = endpoint.Value.responseClass;
                    bool isPost = endpoint.Value.isPost;
                    var requestTrue = (BaseRequest)Activator.CreateInstance(endpoint.Value.requestClass);
                    requestTrue = request;

                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {                            

                            string jsonResponse = isPost 
                            ? await PostDataAsync(urlApiEndpoint, requestTrue.ToJsonBody()) 
                            : await GetDataAsync(urlApiEndpoint, requestTrue.ToQueryParams());

                            var parsedData = JsonParser.ParseResponse(jsonResponse, wildberriesResponse);

                            Console.WriteLine($"Response Size for {tableNameInDatabase}: {jsonResponse.Length}bytes");

                            if (parsedData is IList list && list.Count == 0)
                            {
                                Console.WriteLine($"No data parsed for {tableNameInDatabase}");
                                return;
                            }

                            Console.WriteLine($"Save in DB: {tableNameInDatabase}");
                            // Вызываем SaveDataAsync<T> через рефлексию с типом wildberriesResponse
                            var saveMethod = typeof(DatabaseService).GetMethod("SaveDataAsync");
                            var genericSaveMethod = saveMethod.MakeGenericMethod(wildberriesResponse);
                            await (Task)genericSaveMethod.Invoke(databaseService, new object[] { tableNameInDatabase, parsedData });
                            Console.WriteLine($"Saved: {tableNameInDatabase}");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error in {tableNameInDatabase}: {ex.Message}");
                        }
                    }));
                }
                await Task.WhenAll(tasks);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
