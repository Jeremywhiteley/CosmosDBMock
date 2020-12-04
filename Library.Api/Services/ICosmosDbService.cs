using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface ICosmosDbService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id);
        Task <bool>AddItemAsync(T item);
        Task <bool>UpdateItemAsync(string id, T item);
        Task <bool>DeleteItemAsync(string id);
    }
}
