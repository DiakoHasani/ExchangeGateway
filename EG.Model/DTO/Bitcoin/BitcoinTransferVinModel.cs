using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class BitcoinTransferVinModel
    {
        [JsonProperty("txid")]
        public string TransactionId { get; set; }
        [JsonProperty("prevout")]
        public BitcoinTransferVinPrevoutModel Prevout { get; set; }
        [JsonProperty("is_coinbase")]
        public bool IsCoinbase { get; set; }
    }
}
