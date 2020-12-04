// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Microsoft.Azure.Cosmos;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Creates a Cosmos DB database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        public static async Task<CosmosService<T>> Initialize<T>(CosmosDBSettings settings, string partitionKey = "/ServiceCountry")
        {
            Trace.WriteLine($"DatabaseInitializer for {typeof(T).Name} key: {partitionKey}");

            var databaseId = settings.DatabaseId;
            var containerId= typeof(T).Name;
            
            var client = new CosmosClient(settings.EndPoint, settings.Key);

            var database = await client.CreateDatabaseIfNotExistsAsync(databaseId);
            await database.Database.CreateContainerIfNotExistsAsync(containerId, partitionKey);

            //await SeedData<T>(client, databaseId, containerId);
            
            //! writing
            var cosmosDbService = new CosmosService<T>(client, databaseId, containerId, partitionKey);

            return cosmosDbService;
        }

        private static async Task SeedData<T>(CosmosClient cosmosClient, string databaseId, string containerId)
        {
            try {
                var container = cosmosClient.GetContainer(databaseId, containerId);

                var query = container.GetItemQueryIterator<dynamic>(new QueryDefinition("SELECT COUNT(1) FROM c"));
                var response = await query.ReadNextAsync();

                //? ?
                //var z = response.Resource;
            }
            catch(Exception e) {
                Trace.WriteLine(e.Message);
            }
           
            
        }
    }
}
