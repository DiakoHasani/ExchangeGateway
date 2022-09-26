using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.TronScan
{
    public class TransferModel
    {
        [JsonProperty("token_transfers")]
        public List<TokenTransferModel> TokenTransfers { get; set; }
    }
}
