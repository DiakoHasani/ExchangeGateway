using EG.Model.DTO.TronScan;
using EG.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Apis
{
    public class TronScanApi : BaseApi, ITronScanApi
    {
        private TransferModel transferModel;
        private HttpResponseMessage responseGetTransfers;

        public async Task<ApiResultModel<List<TokenTransferModel>>> GetTransfers(GetTransferRequestModel model)
        {
            try
            {
                responseGetTransfers = await Get($"{TronScanUrl}api/token_trc20/transfers?limit={model.Limit}&start=0&sort=-timestamp&count=true&toAddress={model.ToAddress}&relatedAddress={model.RelatedAddress}");

                if (!responseGetTransfers.IsSuccessStatusCode)
                {
                    return new ApiResultModel<List<TokenTransferModel>>
                    {
                        Messages = new List<string>
                        {
                            await responseGetTransfers.Content.ReadAsStringAsync()
                        }
                    };
                }

                transferModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TransferModel>(await responseGetTransfers.Content.ReadAsStringAsync());

                if (transferModel == null || transferModel.TokenTransfers == null)
                {
                    return new ApiResultModel<List<TokenTransferModel>>
                    {
                        Messages = new List<string>
                        {
                            "transferModel in GetTransfers Tron is null"
                        }
                    };
                }

                return new ApiResultModel<List<TokenTransferModel>>
                {
                    Result = true,
                    Messages = new List<string>
                    {
                        "success call GetTransfers Tron"
                    },
                    Response = transferModel.TokenTransfers
                };
            }
            catch (Exception ex)
            {
                return new ApiResultModel<List<TokenTransferModel>>
                {
                    Messages = new List<string> { ex.Message }
                };
            }
        }
    }
    public interface ITronScanApi
    {
        Task<ApiResultModel<List<TokenTransferModel>>> GetTransfers(GetTransferRequestModel model);
    }
}
