using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DIYHIIT.Contracts;
using DIYHIIT.Exceptions;
using Newtonsoft.Json;

namespace DIYHIIT.Repository 
{
    public class GenericRepository : IGenericRepository
    {
        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(uri);
        }

        /// <summary>
        /// Send a request to API for object of type T using URL.
        /// </summary>
        /// <typeparam name="T">Desired type of object to be retrieved from Database.</typeparam>
        /// <param name="uri">Endpoint URI to retrieve data from.</param>
        /// <param name="authToken">Authentication token.</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string uri, string authToken = "") where T : new()
        {
            try
            {
                Debug.WriteLine("Geting from client: " + uri);

                HttpClient client = CreateHttpClient(uri);
                HttpResponseMessage responseMessage = await client.GetAsync(uri);

                var resp = await responseMessage.Content.ReadAsStringAsync();

                Debug.WriteLine($"Client response: {resp}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    string message = await responseMessage.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<T>(message);
                    return result;
                }

                // Error
                var content = await responseMessage.Content.ReadAsStringAsync();
                
                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new ServiceAuthenticationException(content);
                }

                Debug.WriteLine($"Error sending data: {content}");

                return default;
                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return default;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                Debug.WriteLine("Posting to client: " + uri);

                HttpClient client = CreateHttpClient(uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Task.Run(async () => await client.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine($"Post successful. Response: {responseMessage.StatusCode}");
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new ServiceAuthenticationException(jsonResult);
                }

                Debug.WriteLine($"Http Post failed. Response:");
                Debug.WriteLine(responseMessage);
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<R> PostAsync<T, R>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Task.Run(async () => await httpClient.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<R>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Task.Run(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private HttpClient CreateHttpClient(string authToken = "")
        {
            var httpClient = new HttpClient();

            var header = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(header);

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);
            }

            return httpClient;
        }
    }
}