using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class VinBitcoinModel
    {
        public string TransactionId { get; set; }
        public string ScriptPubkey { get; set; }
        public string ScriptPubKeyAddress { get; set; }
        public double Value { get; set; }
        public bool IsCoinbase { get; set; }
    }
}
