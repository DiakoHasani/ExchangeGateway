using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Wallet
{
    public class WalletModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public CoinTypeEnum CoinType { get; set; }
        public TokenTypeEnum? TokenType { get; set; }
        public string Last_Txid { get; set; }
        public string WebhookAddress { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
