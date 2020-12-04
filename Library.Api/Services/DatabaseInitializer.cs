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
        public static async Task<CosmosDbService<T>> Initialize<T>(CosmosDBSettings settings)
        {
            Trace.WriteLine($"DatabaseInitializer for {typeof(T).Name}");

            var databaseName = settings.DatabaseId;
            var containerName = typeof(T).Name;
            
            var client = new CosmosClient(settings.EndPoint, settings.Key);

            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id"); //? /id .. whats for?

            var cosmosDbService = new CosmosDbService<T>(client, databaseName, containerName);
            return cosmosDbService;
        }
    }
}
