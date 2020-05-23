using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient Client;
        protected readonly string ServiceAddress;

        private bool _disposed;

        protected BaseClient(IConfiguration configuration, string serviceAddress)
        {
            ServiceAddress = serviceAddress;
            Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                }
            };
        }

        protected T Get<T>(string url) /*where T : new()*/
        {
            return GetAsync<T>(url).Result;
        }

        protected async Task<T> GetAsync<T>(string url, CancellationToken cancel = default) /*where T : new()*/
        {
            var response = await Client.GetAsync(url, cancel);

            //if (response.IsSuccessStatusCode)
            //    return await response.Content.ReadAsAsync<T>(cancel);

            //return new T();

            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>(cancel);
        }

        protected HttpResponseMessage Post<T>(string url, T item)
        {
            return PostAsync(url, item).Result;
        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancel = default)
        {
            var response = await Client.PostAsJsonAsync(url, item, cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item)
        {
            return PutAsync(url, item).Result;
        }

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancel = default)
        {
            var response = await Client.PutAsJsonAsync(url, item, cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url)
        {
            return DeleteAsync(url).Result;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancel = default)
        {
            return await Client.DeleteAsync(url, cancel);
        }

        //~BaseClient() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            _disposed = true;
            Client.Dispose();
        }
    }
}