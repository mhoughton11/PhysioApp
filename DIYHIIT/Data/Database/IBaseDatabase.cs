using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIYHIIT.Data
{
    public interface IBaseDatabase<T>
    {
        string Name { get; set; }
        Task<int> GetItemCount();
        Task<T> GetItemAsync(int id);
        Task<T> GetItemAsync(string name);
        Task<List<T>> GetItemsAsync();
        Task<List<T>> GetItemsAsync(string type);
        Task<int> SaveItemAsync(T item);
        Task<int> SaveItemsAsync(ICollection<T> items);
        Task<int> SaveItemsAsync(IList<T> items);
        Task<int> DeleteItemAsync(T item);
        Task<int> ClearItemsAsync();
    }
}
