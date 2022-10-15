using EG.Model.DTO.Bitcoin;
using EG.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Apis
{
    public class BitcoinApi : BaseApi, IBitcoinApi
    {
        private HttpResponseMessage responseGetTransfers;
        public async Task<ApiResultModel<List<BitcoinTransferModel>>> GetTransafers(string walletAddress)
        {
            try
            {
                responseGetTransfers = await Get($"{BtcScanUrl}{walletAddress}/txs");
                if (responseGetTransfers == null)
                {
                    return new ApiResultModel<List<BitcoinTransferModel>> { Messages = new List<string>() { "response BitcoinApi.GetTransafers is null" } };
                }

                if (!responseGetTransfers.IsSuccessStatusCode)
                {
                    return new ApiResultModel<List<BitcoinTransferModel>> { Messages = new List<string> { "error in call BitcoinApi.GetTransafers error message: " + await responseGetTransfers.Content.ReadAsStringAsync() } };
                }

                return new ApiResultModel<List<BitcoinTransferModel>>
                {
                    Result = true,
                    Messages = new List<string> { "success call BitcoinApi.GetTransafers api" },
                    Response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BitcoinTransferModel>>(await responseGetTransfers.Content.ReadAsStringAsync())
                };
            }
            catch (Exception ex)
            {
                return new ApiResultModel<List<BitcoinTransferModel>> { Messages = new List<string> { ex.Message } };
            }
        }
    }
    public interface IBitcoinApi
    {
        Task<ApiResultModel<List<BitcoinTransferModel>>> GetTransafers(string walletAddress);
    }
}
