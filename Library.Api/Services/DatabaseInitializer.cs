// ******************************
// Axis Project
// @__harveyt__
// ******************************
using Library.Shared;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Creates a Cosmos database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        public static async Task<CosmosService<T>> Initialize<T>(CosmosSettings settings)
        {
            Trace.WriteLine($"DatabaseInitializer for {typeof(T).Name} key: {settings.PartitionName}");

            var databaseId = settings.DatabaseId;
            var containerId = typeof(T).Name;

            var client = new CosmosClient(settings.EndPoint, settings.Key);

            var database = await client.CreateDatabaseIfNotExistsAsync(databaseId);
            var container = await database.Database.CreateContainerIfNotExistsAsync(containerId, "/" + settings.PartitionName);

            // SEED
            await SeedData<T>(container, settings);

            // OBJECT FOR DI
            var cosmosDbService = new CosmosService<T>(container, settings.PartitionName);

            return cosmosDbService;
        }

        private static async Task SeedData<T>(Container container, CosmosSettings settings)
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
                Trace.WriteLine($"** Count of {typeof(T).Name}: {count}");

                if (count == 0) {
                    // insert data from static file
                    var file = $"{Startup.PATH}/Data/{typeof(T).Name}_SEED.json";
                    if (File.Exists(file)) {
                        // here, has to use Newtonsoft.Json
                        var data = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(file));
                        // partitionKey data
                        var pkValue = Utils.COUNTRYID;
                        var pkName = settings.PartitionName;
                        var pk = new PartitionKey(pkValue);
                        // insert items
                        foreach (T item in data) {
                            Utils.SetValue<T>(item, pkName, pkValue);
                            await container.CreateItemAsync(item, pk);
                            //
                            Trace.WriteLine($"** Created: {item}");
                        }
                    }
                }
            }
            catch (Exception e) {
                Trace.WriteLine($"** Exception: {e.Message}");
            }
        }
    }
}
