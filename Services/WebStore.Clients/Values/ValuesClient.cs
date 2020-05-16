using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValueServices
    {
        public ValuesClient(IConfiguration Configuration) : base(Configuration, "api/values")
        {
        }

        public IEnumerable<string> Get()
        {
            var response = Client.GetAsync(ServiceAddress).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var response = Client.GetAsync($"{ServiceAddress}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<string>().Result;

            return string.Empty;
        }

        public Uri Post(string value)
        {
            var response = Client.PostAsJsonAsync($"{ServiceAddress}/post", value).Result;
            return response.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, string value)
        {
            var response = Client.PutAsJsonAsync($"{ServiceAddress}/put/{id}", value).Result;
            return response.EnsureSuccessStatusCode().StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            var response = Client.DeleteAsync($"{ServiceAddress}/delete/{id}").Result;
            return response.StatusCode;
        }
    }
}