using AutoMapper;
using EG.Business.Apis;
using EG.General.Enums;
using EG.Model.DTO.Bitcoin;
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
    public class BitcoinBusiness : IBitcoinBusiness
    {
        private readonly IBitcoinRepository _bitcoinRepository;
        private readonly IMapper _mapper;
        private readonly IBitcoinApi _bitcoinApi;
        private readonly IErrorBusiness _errorBusiness;
        private readonly IWalletBusiness _walletBusiness;
        private readonly IVinBitcoinBusiness _vinBitcoinBusiness;
        private readonly IVoutBitcoinBusiness _voutBitcoinBusiness;
        private readonly IWebhookRequestBusiness _webhookRequestBusiness;

        private ApiResultModel<List<BitcoinTransferModel>> resultLastTransfers;
        private List<BitcoinTransferIndexModel> transfers;
        private MessageModel<List<BitcoinTransferModel>> resultGetLastTransfers;

        public BitcoinBusiness(IBitcoinRepository bitcoinRepository,
            IMapper mapper,
            IBitcoinApi bitcoinApi,
            IErrorBusiness errorBusiness,
            IWalletBusiness walletBusiness,
            IVinBitcoinBusiness vinBitcoinBusiness,
            IVoutBitcoinBusiness voutBitcoinBusiness,
            IWebhookRequestBusiness webhookRequestBusiness)
        {
            _bitcoinRepository = bitcoinRepository;
            _mapper = mapper;
            _bitcoinApi = bitcoinApi;
            _errorBusiness = errorBusiness;
            _walletBusiness = walletBusiness;
            _vinBitcoinBusiness = vinBitcoinBusiness;
            _voutBitcoinBusiness = voutBitcoinBusiness;
            _webhookRequestBusiness = webhookRequestBusiness;
        }

        public MessageModel<int> AddBitcoin(BitcoinTransferModel model, int walletId)
        {
            try
            {
                var bitcoin = new TblBitcoin
                {
                    Confirmed = model.Status.Confirmed,
                    Fee = model.Fee,
                    TransactionId = model.TransactionId,
                    WalletId = walletId
                };
                _bitcoinRepository.Add(bitcoin);
                if (!(_bitcoinRepository.SaveChange() > 0))
                    return new MessageModel<int> { Messages = new List<string> { "error in add bitcoin to database TransactionId: " + model.TransactionId } };

                return new MessageModel<int>
                {
                    Result = true,
                    Data = bitcoin.Id,
                    Messages = new List<string> { "success add bitcoin to database" }
                };
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel<int> { Messages = new List<string> { "error in main try catch AddBitcoin exception message: " + ex.Message } };
            }
        }

        public async Task<MessageModel> CallBtcScan(WalletModel model)
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
                        var resultAddBitcoin = AddBitcoin(resultGetLastTransfers.Data[i], model.Id);
                        result.Messages.AddRange(resultAddBitcoin.Messages);
                        if (resultAddBitcoin.Result)
                        {
                            var resultAddVinBitcoins = _vinBitcoinBusiness.AddRange(resultGetLastTransfers.Data[i].Vin, resultAddBitcoin.Data);
                            result.Messages.AddRange(resultAddVinBitcoins.Messages);

                            var resultAddVoutBitcoin = _voutBitcoinBusiness.AddRange(resultGetLastTransfers.Data[i].Vout, resultAddBitcoin.Data);
                            result.Messages.AddRange(resultAddVoutBitcoin.Messages);

                            var resultAddWebhook = _webhookRequestBusiness.AddWebhook(new AddWebhookRequestModel
                            {
                                BitcoinId = resultAddBitcoin.Data,
                                CoinType = CoinTypeEnum.BitCoin,
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
                    }
                    catch (Exception ex)
                    {
                        _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                        result.Messages.Add($"error in add Bitcoin to database txtid: {resultGetLastTransfers.Data[i].TransactionId} -- exception message: {ex.Message}");
                    }
                }
                result.Result = true;
                return result;
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel { Messages = new List<string> { "exception in main try catch CallBtcScan exception message: " + ex.Message } };
            }
        }

        public async Task<MessageModel<List<BitcoinTransferModel>>> GetLastTransfers(WalletModel model)
        {
            try
            {
                resultLastTransfers = await _bitcoinApi.GetTransafers(model.Address);
                if (!resultLastTransfers.Result)
                {
                    _errorBusiness.AddError(resultLastTransfers.Messages, "BitcoinApi.GetTransafers");
                    return new MessageModel<List<BitcoinTransferModel>> { Messages = resultLastTransfers.Messages };
                }
                if (resultLastTransfers.Response == null || resultLastTransfers.Response.Count == 0)
                {
                    return new MessageModel<List<BitcoinTransferModel>> { Messages = new List<string> { $"{model.Address} wallet has not transactions" } };
                }
                if (string.IsNullOrWhiteSpace(model.Last_Txid))
                {
                    _walletBusiness.UpdateTxId(new UpdateTransactionIdBodyModel { Txid = resultLastTransfers.Response.FirstOrDefault().TransactionId, WalletId = model.Id });
                    return new MessageModel<List<BitcoinTransferModel>> { Messages = new List<string> { $"The last transaction ID was recorded for {model.Address} wallet" } };
                }
                else
                {
                    transfers = resultLastTransfers.Response.Select((item, index) => new BitcoinTransferIndexModel { Index = index, Item = item }).ToList();
                }

                var transferIndex = transfers.Count;

                if (transfers.Any(a => a.Item.TransactionId == model.Last_Txid))
                {
                    transferIndex = transfers.Where(a => a.Item.TransactionId == model.Last_Txid).FirstOrDefault().Index;
                }

                var result = new MessageModel<List<BitcoinTransferModel>>();
                result.Data = new List<BitcoinTransferModel>();

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
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel<List<BitcoinTransferModel>> { Messages = new List<string> { ex.Message } };
            }
        }
    }
    public interface IBitcoinBusiness
    {
        Task<MessageModel<List<BitcoinTransferModel>>> GetLastTransfers(WalletModel model);
        Task<MessageModel> CallBtcScan(WalletModel model);
        MessageModel<int> AddBitcoin(BitcoinTransferModel model, int walletId);
    }
}
