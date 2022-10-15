using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class BitcoinTransferModel
    {
        [JsonProperty("txid")]
        public string TransactionId { get; set; }
        [JsonProperty("vin")]
        public List<BitcoinTransferVinModel> Vin { get; set; }
        [JsonProperty("vout")]
        public List<BitcoinTransferVoutModel> Vout { get; set; }
        [JsonProperty("fee")]
        public double Fee { get; set; }
        [JsonProperty("status")]
        public BitcoinTransferStatusModel Status { get; set; }
    }
}
