using EG.Model.DTO.Bitcoin;
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
    public class VinBitcoinBusiness : IVinBitcoinBusiness
    {
        private readonly IVinBitcoinRepository _vinBitcoinRepository;
        private readonly IErrorBusiness _errorBusiness;
        public VinBitcoinBusiness(IVinBitcoinRepository vinBitcoinRepository,
            IErrorBusiness errorBusiness)
        {
            _vinBitcoinRepository = vinBitcoinRepository;
            _errorBusiness = errorBusiness;
        }

        public MessageModel AddRange(List<BitcoinTransferVinModel> models, int btcId)
        {
            var result = new MessageModel();

            foreach (var item in models)
            {
                try
                {
                    _vinBitcoinRepository.Add(new TblVinBitcoin
                    {
                        BitcoinId = btcId,
                        IsCoinbase = item.IsCoinbase,
                        ScriptPubkey = item.Prevout.ScriptPubkey,
                        ScriptPubKeyAddress = item.Prevout.ScriptPubKeyAddress,
                        TransactionId = item.TransactionId,
                        Value = item.Prevout.Value
                    });
                    if (_vinBitcoinRepository.SaveChange() > 0)
                    {
                        result.Messages.Add($"success add VinBitcoin to database. TransactionId: {item.TransactionId}");
                    }
                    else
                    {
                        result.Messages.Add($"error in add VinBitcoin to database. TransactionId: {item.TransactionId}");
                    }
                }
                catch (Exception ex)
                {
                    _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                    result.Messages.Add($"error in main try catch VinBitcoinBusiness.AddRange TransactionId: {item.TransactionId}");
                }
            }

            return result;
        }
    }
    public interface IVinBitcoinBusiness
    {
        MessageModel AddRange(List<BitcoinTransferVinModel> models, int btcId);
    }
}
