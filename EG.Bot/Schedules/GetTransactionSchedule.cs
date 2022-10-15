using EG.Business.Services;
using EG.General.Enums;
using EG.General.Helpers;
using EG.Model.DTO.Wallet;
using EG.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EG.Bot.Schedules
{
    public class GetTransactionSchedule : IGetTransactionSchedule
    {
        private readonly IErrorBusiness _errorBusiness;
        private readonly ITronBusiness _tronBusiness;
        private readonly IWalletBusiness _walletBusiness;
        private readonly IWebhookRequestBusiness _webhookRequestBusiness;
        private readonly IBitcoinBusiness _bitcoinBusiness;

        private bool pause = false;
        private List<WalletModel> wallets;
        MessageModel resultCallTronScan, resultCallBtcScan, resultCallWebhook;

        public GetTransactionSchedule(IErrorBusiness errorBusiness,
            ITronBusiness tronBusiness,
            IWalletBusiness walletBusiness,
            IWebhookRequestBusiness webhookRequestBusiness,
            IBitcoinBusiness bitcoinBusiness)
        {
            _errorBusiness = errorBusiness;
            _tronBusiness = tronBusiness;
            _walletBusiness = walletBusiness;
            _webhookRequestBusiness = webhookRequestBusiness;
            _bitcoinBusiness = bitcoinBusiness;
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowMessage(List<string> messages)
        {
            messages.ForEach(message => Console.WriteLine(message));
        }

        public async Task Start()
        {
            while (true)
            {
                if (!pause)
                {
                    try
                    {
                        pause = true;

                        ShowMessage("start get Wallets in database");
                        wallets = _walletBusiness.GetWallets();

                        #region Call Tron

                        ShowMessage("start call TronScan");
                        foreach (var wallet in wallets.Where(a => a.CoinType == CoinTypeEnum.Tron))
                        {
                            resultCallTronScan = await _tronBusiness.CallTronScan(wallet);
                            ShowMessage(resultCallTronScan.Messages);
                        }

                        #endregion

                        #region Call Bitcoin

                        ShowMessage("start call BtcScan");
                        foreach (var wallet in wallets.Where(a => a.CoinType == CoinTypeEnum.BitCoin))
                        {
                            resultCallBtcScan = await _bitcoinBusiness.CallBtcScan(wallet);
                            ShowMessage(resultCallBtcScan.Messages);
                        }
                        #endregion

                        #region Call Webhook
                        ShowMessage("start call webhook");
                        resultCallWebhook = await _webhookRequestBusiness.CallWebhook();
                        ShowMessage(resultCallWebhook.Messages);
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                        ShowMessage("error in main try catch start excption message: " + ex.Message);
                    }
                    finally
                    {
                        ShowMessage("---------------------------------------------------------");
                        Thread.Sleep(60000);
                        pause = false;
                    }
                }
            }
        }
    }
    public interface IGetTransactionSchedule
    {
        Task Start();
        void ShowMessage(string message);
        void ShowMessage(List<string> messages);
    }
}
