// ******************************
// Axis Project
// @__harveyt__
// ******************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Library.Shared;
using Microsoft.Azure.Cosmos;

namespace LibraryApi.Services
{
    /// <summary>
    /// Generic CosmosDB data handler
    /// </summary>
    public class CosmosService<T> : ICosmosService<T>
    {
        readonly Container _container;
        readonly string _partitionName;

        public CosmosService(Container container, string partitionName)
        {
            _container = container;
            _partitionName = partitionName;
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try {
                var response = await _container.CreateItemAsync(item, GetPartitionKey(item));
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        public async Task<T> GetItemAsync(string id, string partition = DEFAULT_PARTITION)
        {
            try {
                // works due to the id is unique
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, GetPartitionKey(partition));
                return response.Resource;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return default;
        }

        public async Task<bool> DeleteItemAsync(string id, string partition = DEFAULT_PARTITION)
        {
            try {
                // works due to the id is unique
                await _container.DeleteItemAsync<T>(id, GetPartitionKey(partition));
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var results = new List<T>();
            try {
                var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
                while (query.HasMoreResults) {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return results;
        }

        public async Task<bool> UpdateItemAsync(string id, T item)
        {
            try {
                await _container.ReplaceItemAsync(item, id, GetPartitionKey(item));
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        private PartitionKey GetPartitionKey(T item)
        {
            if (string.IsNullOrEmpty(Utils.GetValue(item, _partitionName))) {
                // solve with default
                Utils.SetValue(item, _partitionName, COUNTRYID);
            }
            return new PartitionKey(Utils.GetValue(item, _partitionName));
        }

        private static PartitionKey GetPartitionKey(string value)
        {
            if (value == DEFAULT_PARTITION || string.IsNullOrEmpty(value)) {
                // solve with default
                value = COUNTRYID;
            }
            return new PartitionKey(value);
        }

        // Acts as the default partition
        static string _country;
        public static string COUNTRYID {
            get {
                if (_country == null) {
                    _country = new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;
                }
                return _country;
            }
            // NOTE. About Partitions
            // https://azure.microsoft.com/en-us/resources/videos/azure-documentdb-elastic-scale-partitioning/
        }

        const string DEFAULT_PARTITION = "COUNTRYID";
    }
}
