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
            var container = await database.Database.CreateContainerIfNotExistsAsync(containerId, partitionKey);

            // SEED
            await SeedData<T>(container);

            // OBJECT FOR DI
            var cosmosDbService = new CosmosService<T>(client, databaseId, containerId, partitionKey);

            return cosmosDbService;
        }

        private static async Task SeedData<T>(Container container)
        {
            try {
                var count = 0;
                var queryDefinition = new QueryDefinition("SELECT VALUE COUNT(1) FROM c");
                var queryResultSetIterator = container.GetItemQueryIterator<int>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults) {
                    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var i in currentResultSet) {
                        count = i;
                    }
                }
                Trace.WriteLine($"Count of {typeof(T).Name}: {count}");

                if (count == 0) {
                    // search a resorce for insert data

                }
            }
            catch (Exception e) {
                Trace.WriteLine($"** Exception: {e.Message}");
            }
        }
    }
}
