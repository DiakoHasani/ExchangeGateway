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
    public class VoutBitcoinBusiness : IVoutBitcoinBusiness
    {
        private readonly IVoutBitcoinRepository _voutBitcoinRepository;
        private readonly IErrorBusiness _errorBusiness;
        public VoutBitcoinBusiness(IVoutBitcoinRepository voutBitcoinRepository,
            IErrorBusiness errorBusiness)
        {
            _voutBitcoinRepository = voutBitcoinRepository;
            _errorBusiness = errorBusiness;
        }

        public MessageModel AddRange(List<BitcoinTransferVoutModel> models, int btcId)
        {
            var result = new MessageModel();
            try
            {
                foreach (var item in models)
                {
                    _voutBitcoinRepository.Add(new TblVoutBitcoin
                    {
                        BitcoinId = btcId,
                        ScriptPubKey = item.ScriptPubKey,
                        ScriptPubKeyAddress = item.ScriptPubKeyAddress,
                        Value = item.Value
                    });
                    if (_voutBitcoinRepository.SaveChange() > 0)
                    {
                        result.Messages.Add("success add VoutBitcoin to database");
                    }
                    else
                    {
                        result.Messages.Add("error in add VoutBitcoin to database");
                    }
                }
            }
            catch (Exception ex)
            {
                _errorBusiness.AddError(ex, MethodBase.GetCurrentMethod().DeclaringType.FullName);
                result.Messages.Add("error in main try catch add VoutBitcoin exception message: " + ex.Message);
            }
            return result;
        }
    }
    public interface IVoutBitcoinBusiness
    {
        MessageModel AddRange(List<BitcoinTransferVoutModel> models, int btcId);
    }
}
