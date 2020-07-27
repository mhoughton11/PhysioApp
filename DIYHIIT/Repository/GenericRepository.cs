using System;
using System.Net.Http;
using System.Threading.Tasks;
using DIYHIIT.Contracts;

namespace DIYHIIT.Repository 
{
    public class GenericRepository : IGenericRepository
    {
        public Task DeleteAsync(string uri, string authToken = "")
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            HttpClient client = new CreateHttpClient(uri);
            HttpResponseMessage responseMessage = await client.GetAsync(uri);
        }

        public Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            throw new NotImplementedException();
        }

        public Task<R> PostAsync<T, R>(string uri, T data, string authToken = "")
        {
            throw new NotImplementedException();
        }

        public Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            throw new NotImplementedException();
        }
    }
}