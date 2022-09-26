using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.TronScan
{
    public class TokenInfoModel
    {
        [JsonProperty("tokenAbbr")]
        public string TokenAbbr { get; set; }

        [JsonProperty("tokenName")]
        public string TokenName { get; set; }

        [JsonProperty("tokenId")]
        public string TokenId { get; set; }
    }
}
