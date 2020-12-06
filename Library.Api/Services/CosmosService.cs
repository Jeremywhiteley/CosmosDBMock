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
        readonly string _partitionValue;
        readonly PartitionKey _partitionKey;

        public CosmosService(Container container)
        {
            _container = container;

            if (_partitionName == "ServiceCountry") {// the model has to have tris property
                _partitionValue = COUNTRYID;
            }
            _partitionKey = new PartitionKey(_partitionValue);
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try {
                // Resolves partition key. Uses reflexion due to uses generic type
                if (string.IsNullOrEmpty(Utils.GetValue(item, _partitionName))) {
                    Utils.SetValue(item, _partitionName, _partitionValue);
                }
                // post
                var response = await _container.CreateItemAsync(item, _partitionKey);
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try {
                await _container.DeleteItemAsync<T>(id, _partitionKey);
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        public async Task<T> GetItemAsync(string id)
        {
            try {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, _partitionKey);
                return response.Resource;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return default;
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
                await _container.ReplaceItemAsync(item, id, _partitionKey);
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        // Acts as the main partition
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
    }
}
