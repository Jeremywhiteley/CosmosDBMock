using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace LibraryApi.Services
{
    public class CosmosDbService<T> : ICosmosDbService<T>
    {
        readonly Container _container;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try {
                var response = await _container.CreateItemAsync<T>(item, new PartitionKey("/Id"));
                return true;
            }
            catch {
            }
            return false;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try {
                await _container.DeleteItemAsync<T>(id, new PartitionKey(id));
                return true;
            }
            catch {
                return false;
            }
        }

        public async Task<T> GetItemAsync(string id)
        {
            try {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException exception) when (exception.StatusCode == System.Net.HttpStatusCode.NotFound) {
                // 
            }
            return default;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults) {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<bool> UpdateItemAsync(string id, T item)
        {
            try {
                await _container.UpsertItemAsync<T>(item, new PartitionKey(id));
                return true;
            }
            catch {
            }
            return false;
        }
    }
}
