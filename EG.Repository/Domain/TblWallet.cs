using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblWallet : BaseEntity
    {
        [MaxLength(length: 300)]
        public string Address { get; set; }

        public CoinTypeEnum CoinType { get; set; }

        public TokenTypeEnum? TokenType { get; set; }

        [MaxLength(length: 200)]
        public string Last_Txid { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        [MaxLength(length: 500)]
        public string WebhookAddress { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<TblSellRequest> TblSellRequests { get; set; }
        public virtual ICollection<TblBitcoin> TblBitcoins { get; set; }
        public virtual ICollection<TblTron> TblTrons { get; set; }
    }
}
