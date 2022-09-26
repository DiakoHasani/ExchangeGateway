using AutoMapper;
using EG.Model.DTO.Wallet;
using EG.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Services
{
    public class WalletBusiness : IWalletBusiness
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        public WalletBusiness(IWalletRepository walletRepository,
            IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public int GetIdByAddress(string address)
        {
            return _walletRepository.GetAll(a => a.Address.Equals(address) && a.Enabled).Select(a => a.Id).FirstOrDefault();
        }

        public List<WalletModel> GetWallets()
        {
            return _mapper.Map<List<WalletModel>>(_walletRepository.GetAll(a => a.Enabled).OrderBy(a => a.CreateDate).ToList());
        }

        public bool UpdateTxId(UpdateTransactionIdBodyModel model)
        {
            var wallet = _walletRepository.GetById(model.WalletId);
            if (wallet == null)
                return false;

            wallet.Last_Txid = model.Txid;
            _walletRepository.Update(wallet);
            return _walletRepository.SaveChange() > 0;
        }
    }
    public interface IWalletBusiness
    {
        List<WalletModel> GetWallets();
        bool UpdateTxId(UpdateTransactionIdBodyModel model);
        int GetIdByAddress(string address);
    }
}
