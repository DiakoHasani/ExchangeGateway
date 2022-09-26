using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.WebhookRequest
{
    public class WebhookRequestModel
    {
        public int Id { get; set; }
        public string Url { get; set; } = "";
        public bool Sended { get; set; } = false;
        public int SendCount { get; set; } = 0;
        public CoinTypeEnum CoinType { get; set; }
        public int? TronId { get; set; }
        public int? BitcoinId { get; set; }
    }
}
