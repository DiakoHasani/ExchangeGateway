using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class VoutBitcoinModel
    {
        public string ScriptPubKey { get; set; }
        public string ScriptPubKeyAddress { get; set; }
        public double Value { get; set; }
    }
}
