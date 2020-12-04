using Microsoft.Azure.Cosmos;
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
        public static async Task<CosmosService<T>> Initialize<T>(CosmosDBSettings settings, string partitionKey = "/id")
        {
            Trace.WriteLine($"DatabaseInitializer for {typeof(T).Name} key: {partitionKey}");

            var databaseName = settings.DatabaseId;
            var containerName = typeof(T).Name;

            var client = new CosmosClient(settings.EndPoint, settings.Key);

            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, partitionKey);

            var cosmosDbService = new CosmosService<T>(client, databaseName, containerName, partitionKey);
            return cosmosDbService;
        }
    }
}
