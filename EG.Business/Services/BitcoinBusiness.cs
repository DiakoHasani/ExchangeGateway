using EG.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Services
{
    public class BitcoinBusiness : IBitcoinBusiness
    {
        private readonly IBitcoinRepository _bitcoinRepository;
        public BitcoinBusiness(IBitcoinRepository bitcoinRepository)
        {
            _bitcoinRepository = bitcoinRepository;
        }
    }
    public interface IBitcoinBusiness
    {
    }
}
