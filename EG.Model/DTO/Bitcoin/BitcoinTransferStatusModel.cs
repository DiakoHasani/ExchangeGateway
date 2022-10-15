using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Bitcoin
{
    public class BitcoinTransferStatusModel
    {
        [JsonProperty("confirmed")]
        public bool Confirmed { get; set; }
    }
}
