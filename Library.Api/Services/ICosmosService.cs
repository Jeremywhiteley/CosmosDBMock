﻿// ******************************
// Axis Project
// @__harveyt__
// ******************************
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface ICosmosService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id, string partition);
        Task <bool>AddItemAsync(T item);
        Task <bool>UpdateItemAsync(string id, T item);
        Task <bool>DeleteItemAsync(string id, string partition);
    }
}
