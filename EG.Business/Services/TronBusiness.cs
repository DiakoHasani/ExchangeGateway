using AutoMapper;
using EG.Business.Apis;
using EG.General.Enums;
using EG.General.Helpers;
using EG.Model.DTO.Tron;
using EG.Model.DTO.TronScan;
using EG.Model.DTO.Wallet;
using EG.Model.DTO.WebhookRequest;
using EG.Model.General;
using EG.Repository.Domain;
using EG.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Services
{
    public class TronBusiness : ITronBusiness
    {
        private readonly IErrorBusiness _errorBusiness;
        private readonly ITronScanApi _tronScanApi;
        private readonly IWalletBusiness _walletBusiness;
        private readonly IWebhookRequestBusiness _webhookRequestBusiness;
        private readonly IMapper _mapper;
        private readonly ITronRepository _tronRepository;

        private ApiResultModel<List<TokenTransferModel>> resultLastTransfer;
        private List<TokenTransferIndexModel> transfers;
        private MessageModel<List<TokenTransferModel>> resultGetLastTransfers;

        public TronBusiness(IErrorBusiness errorBusiness,
            ITronScanApi tronScanApi,
            IWalletBusiness walletBusiness,
            IWebhookRequestBusiness webhookRequestBusiness,
            IMapper mapper,
            ITronRepository tronRepository)
        {
            _errorBusiness = errorBusiness;
            _tronScanApi = tronScanApi;
            _walletBusiness = walletBusiness;
            _webhookRequestBusiness = webhookRequestBusiness;
            _mapper = mapper;
            _tronRepository = tronRepository;
        }

        public MessageModel<int> AddTron(TokenTransferModel model)
        {
            try
            {
                var tron = _mapper.Map<TblTron>(model);
                tron.TokenId = model.TokenInfo.TokenId;
                tron.TokenAbbr = model.TokenInfo.TokenAbbr;
                tron.WalletId = _walletBusiness.GetIdByAddress(model.ToAddress);
                tron.TokenType = (TokenTypeEnum)model.TokenInfo.TokenAbbr.GetEnumByName();
                _tronRepository.Add(tron);
                if (!(_tronRepository.SaveChange() > 0))
                {
                    return new MessageModel<int> { Messages = new List<string> { "error in add tron to database" } };
                }

                return new MessageModel<int>
                {
                    Result = true,
                    Data = tron.Id,
                    Messages = new List<string> { "success add tron to database" }
                };
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel<int> { Messages = new List<string> { "error in main try catch AddTron exception message: " + ex.Message } };
            }
        }

        public async Task<MessageModel> CallTronScan(WalletModel model)
        {
            try
            {
                resultGetLastTransfers = await GetLastTransfers(model);
                if (!resultGetLastTransfers.Result || resultGetLastTransfers.Data.Count == 0)
                {
                    return new MessageModel { Messages = resultGetLastTransfers.Messages };
                }

                var result = new MessageModel();

                for (int i = 0; i < resultGetLastTransfers.Data.Count; i++)
                {
                    try
                    {
                        var resultAddTron = AddTron(resultGetLastTransfers.Data[i]);
                        if (resultAddTron.Result)
                        {
                            var resultAddWebhook = _webhookRequestBusiness.AddWebhook(new AddWebhookRequestModel
                            {
                                CoinType = CoinTypeEnum.Tron,
                                TronId = resultAddTron.Data,
                                Url = model.WebhookAddress
                            });

                            if (resultAddWebhook)
                            {
                                result.Messages.Add("success add webhook to database txid: " + resultGetLastTransfers.Data[i].TransactionId);

                                if (_walletBusiness.UpdateTxId(new UpdateTransactionIdBodyModel { WalletId = model.Id, Txid = resultGetLastTransfers.Data[i].TransactionId }))
                                {
                                    result.Messages.Add($"success update LastTransactionId Wallet Address= {model.Address}");
                                }
                                else
                                {
                                    result.Messages.Add($"error in update LastTransactionId Wallet Address= {model.Address}");
                                }
                            }
                            else
                            {
                                result.Messages.Add("error in add webhook to database txtid: " + resultGetLastTransfers.Data[i].TransactionId);
                            }
                        }
                        else
                        {
                            result.Messages.Add($"error in add Tron to database txtid: {resultGetLastTransfers.Data[i].TransactionId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                        result.Messages.Add($"error in add Tron to database txtid: {resultGetLastTransfers.Data[i].TransactionId} -- exception message: {ex.Message}");
                    }
                }

                result.Result = true;
                return result;
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel { Messages = new List<string> { "exception in main try catch CallTronScan exception message: " + ex.Message } };
            }
        }

        public async Task<MessageModel<List<TokenTransferModel>>> GetLastTransfers(WalletModel model)
        {
            try
            {
                resultLastTransfer = await _tronScanApi.GetTransfers(new GetTransferRequestModel { RelatedAddress = model.Address, ToAddress = model.Address, Limit = 20 });
                if (!resultLastTransfer.Result)
                {
                    return new MessageModel<List<TokenTransferModel>> { Messages = resultLastTransfer.Messages };
                }

                if (resultLastTransfer.Response == null || resultLastTransfer.Response.Count == 0)
                {
                    return new MessageModel<List<TokenTransferModel>> { Messages = new List<string> { $"{model.Address} wallet has not transactions" } };
                }

                if (string.IsNullOrWhiteSpace(model.Last_Txid))
                {
                    _walletBusiness.UpdateTxId(new UpdateTransactionIdBodyModel { Txid = resultLastTransfer.Response.FirstOrDefault().TransactionId, WalletId = model.Id });
                    return new MessageModel<List<TokenTransferModel>> { Messages = new List<string> { $"The last transaction ID was recorded for {model.Address} wallet" } };
                }
                else
                {
                    transfers = resultLastTransfer.Response.Select((item, index) => new TokenTransferIndexModel { Item = item, Index = index }).ToList();
                    var transferIndex = transfers.Count;

                    if (transfers.Any(a => a.Item.TransactionId == model.Last_Txid))
                    {
                        transferIndex = transfers.Where(a => a.Item.TransactionId == model.Last_Txid).FirstOrDefault().Index;
                    }

                    var result = new MessageModel<List<TokenTransferModel>>();
                    result.Data = new List<TokenTransferModel>();

                    for (int i = transferIndex; i >= 0; i--)
                    {
                        if (!transfers[i].Item.TransactionId.Equals(model.Last_Txid))
                        {
                            result.Data.Add(transfers[i].Item);
                        }
                    }

                    if (result.Data.Count == 0)
                    {
                        result.Messages.Add($"No new transactions have been recorded for {model.Address} wallet");
                    }
                    else
                    {
                        result.Messages.Add($"Success get transactions for {model.Address} wallet");
                    }

                    result.Result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel<List<TokenTransferModel>> { Messages = new List<string> { ex.Message } };
            }
        }

        public TronModel GetTronById(int id)
        {
            return _mapper.Map<TronModel>(_tronRepository.GetById(id));
        }
    }
    public interface ITronBusiness
    {
        Task<MessageModel<List<TokenTransferModel>>> GetLastTransfers(WalletModel model);
        Task<MessageModel> CallTronScan(WalletModel model);
        MessageModel<int> AddTron(TokenTransferModel model);
        TronModel GetTronById(int id);
    }
}
