using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class BitcoinTransferVinPrevoutModel
    {
        [JsonProperty("scriptpubkey")]
        public string ScriptPubkey { get; set; }

        [JsonProperty("scriptpubkey_address")]
        public string ScriptPubKeyAddress { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
