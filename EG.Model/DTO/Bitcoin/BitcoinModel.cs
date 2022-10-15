using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class BitcoinModel
    {
        public string TransactionId { get; set; }
        public double Fee { get; set; }
        public bool Confirmed { get; set; }
        public string WalletAddress { get; set; }
        public List<VinBitcoinModel> VinBitcoins { get; set; }
        public List<VoutBitcoinModel> VoutBitcoins { get; set; }
    }
}
