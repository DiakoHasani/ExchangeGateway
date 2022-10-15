using AutoMapper;
using EG.Business.Apis;
using EG.General.Enums;
using EG.Model.DTO.Bitcoin;
using EG.Model.DTO.Tron;
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
    public class WebhookRequestBusiness : IWebhookRequestBusiness
    {
        private readonly IWebhookRequestRepository _webhookRequestRepository;
        private readonly IMapper _mapper;
        private readonly IWebhookApi _webhookApi;
        private readonly IErrorBusiness _errorBusiness;
        private readonly ITronRepository _tronRepository;
        private readonly IBitcoinRepository _bitcoinRepository;
        private readonly IVinBitcoinRepository _vinBitcoinRepository;
        private readonly IVoutBitcoinRepository _voutBitcoinRepository;
        public WebhookRequestBusiness(IWebhookRequestRepository webhookRequestRepository,
            IMapper mapper,
            IWebhookApi webhookApi,
            IErrorBusiness errorBusiness,
            ITronRepository tronRepository,
            IBitcoinRepository bitcoinRepository,
            IVinBitcoinRepository vinBitcoinRepository,
            IVoutBitcoinRepository voutBitcoinRepository)
        {
            _webhookRequestRepository = webhookRequestRepository;
            _mapper = mapper;
            _webhookApi = webhookApi;
            _errorBusiness = errorBusiness;
            _tronRepository = tronRepository;
            _bitcoinRepository = bitcoinRepository;
            _vinBitcoinRepository = vinBitcoinRepository;
            _voutBitcoinRepository = voutBitcoinRepository;
        }

        public bool AddRangeWebhook(List<AddWebhookRequestModel> model)
        {
            _webhookRequestRepository.AddRange(_mapper.Map<List<TblWebhookRequest>>(model));
            return _webhookRequestRepository.SaveChange() > 0;
        }

        public bool AddWebhook(AddWebhookRequestModel model)
        {
            _webhookRequestRepository.Add(_mapper.Map<TblWebhookRequest>(model));
            return _webhookRequestRepository.SaveChange() > 0;
        }

        public async Task<MessageModel> CallBitcoinWebhook(List<WebhookRequestModel> webhookRequests)
        {
            try
            {
                var result = new MessageModel();
                foreach (var webhookRequestModel in webhookRequests)
                {
                    try
                    {
                        var bitcoin = _bitcoinRepository.GetById(webhookRequestModel.BitcoinId ?? 0);

                        var bitcoinModel = _mapper.Map<BitcoinModel>(bitcoin);
                        bitcoinModel.VinBitcoins = _mapper.Map<List<VinBitcoinModel>>(_vinBitcoinRepository.GetAll(a => a.BitcoinId == webhookRequestModel.BitcoinId).ToList());
                        bitcoinModel.VoutBitcoins = _mapper.Map<List<VoutBitcoinModel>>(_voutBitcoinRepository.GetAll(a => a.BitcoinId == webhookRequestModel.BitcoinId).ToList());
                        bitcoinModel.WalletAddress = bitcoin.Wallet.Address;

                        var resultCallApi = await _webhookApi.CallBitcoin(webhookRequestModel.Url, bitcoinModel);
                        var webhookRequest = _webhookRequestRepository.GetById(webhookRequestModel.Id);

                        if (resultCallApi.Result)
                        {
                            if ((resultCallApi.Response ?? "").Equals("ok"))
                            {
                                webhookRequest.Sended = true;
                                result.Messages.Add("sended webhookRequest Bitcoin txId= " + bitcoin.TransactionId);
                            }
                            else
                            {
                                result.Messages.Add($"sended webhookRequest and error message from client is {resultCallApi.Response} . txId= {bitcoin.TransactionId}");
                            }
                        }
                        else
                        {
                            result.Messages = resultCallApi.Messages;
                        }
                        webhookRequest.SendCount++;
                        _webhookRequestRepository.Update(webhookRequest);
                        if (_webhookRequestRepository.SaveChange() > 0)
                        {
                            result.Result = true;
                            result.Messages.Add("updated webhookRequest in database");
                        }
                        else
                        {
                            result.Messages.Add("error in update webhookRequest in database");
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                        result.Messages.Add(ex.Message);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel { Messages = new List<string> { ex.Message } };
            }
        }

        public async Task<MessageModel> CallTronWebhook(List<WebhookRequestModel> webhookRequests)
        {
            try
            {
                var result = new MessageModel();
                foreach (var webhookRequestModel in webhookRequests)
                {
                    try
                    {
                        var tron = _mapper.Map<TronModel>(_tronRepository.GetById(webhookRequestModel.TronId ?? 0));
                        if (tron != null)
                        {
                            var resultCallApi = await _webhookApi.CallTron(webhookRequestModel.Url, tron);
                            var webhookRequest = _webhookRequestRepository.GetById(webhookRequestModel.Id);

                            if (resultCallApi.Result)
                            {
                                if ((resultCallApi.Response ?? "").Equals("ok"))
                                {
                                    webhookRequest.Sended = true;
                                    result.Messages.Add("sended webhookRequest Tron txId= " + tron.TransactionId);
                                }
                                else
                                {
                                    result.Messages.Add($"sended webhookRequest and error message from client is {resultCallApi.Response} . txId= {tron.TransactionId}");
                                }
                            }
                            else
                            {
                                result.Messages = resultCallApi.Messages;
                            }
                            webhookRequest.SendCount++;
                            _webhookRequestRepository.Update(webhookRequest);
                            if (_webhookRequestRepository.SaveChange() > 0)
                            {
                                result.Result = true;
                                result.Messages.Add("updated webhookRequest in database");
                            }
                            else
                            {
                                result.Messages.Add("error in update webhookRequest in database");
                            }
                        }
                        else
                        {
                            result.Messages.Add("tron is null webhookRequestId= " + webhookRequestModel.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                        result.Messages.Add(ex.Message);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel { Messages = new List<string> { ex.Message } };
            }
        }

        public async Task<MessageModel> CallWebhook()
        {
            try
            {
                var result = new MessageModel();

                var webhookRequests = GetWebhookRequests();

                //در اینجا وب هوک های ترون فراخوانی می شود
                result.Messages.AddRange((await CallTronWebhook(webhookRequests.Where(a => a.CoinType == CoinTypeEnum.Tron).ToList())).Messages);

                //در اینجا وب هوک های بیت کوین فراخوانی می شود
                result.Messages.AddRange((await CallBitcoinWebhook(webhookRequests.Where(a => a.CoinType == CoinTypeEnum.BitCoin).ToList())).Messages);

                return result;
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                return new MessageModel { Messages = new List<string> { "error in main try catch CallWebhook exception message: " + ex.Message } };
            }
        }

        public List<WebhookRequestModel> GetWebhookRequests()
        {
            var date = DateTime.Now.AddDays(-2);
            return _mapper.Map<List<WebhookRequestModel>>(_webhookRequestRepository.GetAll(a => a.CreateDate >= date && !a.Sended && a.SendCount < 10).AsEnumerable());
        }
    }
    public interface IWebhookRequestBusiness
    {
        bool AddWebhook(AddWebhookRequestModel model);
        bool AddRangeWebhook(List<AddWebhookRequestModel> model);
        Task<MessageModel> CallWebhook();
        List<WebhookRequestModel> GetWebhookRequests();
        Task<MessageModel> CallTronWebhook(List<WebhookRequestModel> webhookRequests);
        Task<MessageModel> CallBitcoinWebhook(List<WebhookRequestModel> webhookRequests);
    }
}
