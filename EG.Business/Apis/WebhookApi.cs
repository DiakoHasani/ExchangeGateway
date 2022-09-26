using EG.Model.DTO.Tron;
using EG.Model.DTO.TronScan;
using EG.Model.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Apis
{
    public class WebhookApi : BaseApi, IWebhookApi
    {
        public async Task<ApiResultModel<string>> CallTron(string url, TronModel model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await Post(url, body);

                if (result == null)
                {
                    return new ApiResultModel<string> { Messages = new List<string> { " result in WebhookApi.CallTron is null" } };
                }

                if (!result.IsSuccessStatusCode)
                {
                    return new ApiResultModel<string> { Messages = new List<string> { await result.Content.ReadAsStringAsync() } };
                }

                return new ApiResultModel<string>
                {
                    Messages = new List<string> { "success call webhook api tron" },
                    Result = true,
                    Response = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync())
                };
            }
            catch (Exception ex)
            {
                return new ApiResultModel<string>
                {
                    Messages = new List<string> { ex.Message }
                };
            }
        }
    }
    public interface IWebhookApi
    {
        Task<ApiResultModel<string>> CallTron(string url, TronModel model);
    }
}
