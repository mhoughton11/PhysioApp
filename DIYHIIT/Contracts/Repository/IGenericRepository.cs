using System;
using System.Threading.Tasks;

namespace DIYHIIT.Contracts
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri,string authToken = "") where T : new();
        Task<T> PostAsync<T>(string uri, T data, string authToken = "");
        Task<T> PutAsync<T>(string uri, T data, string authToken = "");
        Task DeleteAsync(string uri, string authToken = "");
        Task<R> PostAsync<T, R>(string uri, T data, string authToken = "");
    }
}