using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace LibraryApi.Services
{
    public class CosmosService<T> : ICosmosService<T>
    {
        readonly Container _container;
        readonly string _partitionKey;

        public CosmosService(CosmosClient cosmosClient, string databaseName, string containerName, string partitionKey)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
            _partitionKey = partitionKey;
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try {
                var response = await _container.CreateItemAsync<T>(item, new PartitionKey(GetValueOf(item, _partitionKey)));
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
                await _container.UpsertItemAsync(item, new PartitionKey(id));
                return true;
            }
            catch {
            }
            return false;
        }

        private static string GetValueOf(T item, string partitionKey)
        {
            return item.GetType().GetProperty(partitionKey).GetValue(item, null).ToString();
        }
    }
}
