using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Apis
{
    public abstract class BaseApi
    {
        protected const string TronScanUrl = "https://apilist.tronscan.org/";

        protected async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await client.PostAsync(url, data);
            }
        }

        protected async Task<HttpResponseMessage> Post(string url, StringContent data)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await client.PostAsync(url, data);
            }
        }

        protected async Task<HttpResponseMessage> Get(string url)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await client.GetAsync(url);
            }
        }

        protected async Task<HttpResponseMessage> Put(string url, StringContent data)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await client.PutAsync(url, data);
            }
        }
    }
}
