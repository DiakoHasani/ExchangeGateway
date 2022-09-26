using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblWebhookRequest : BaseEntity
    {
        public string Url { get; set; } = "";
        public bool Sended { get; set; } = false;
        public int SendCount { get; set; } = 0;
        public CoinTypeEnum CoinType { get; set; }
        public int? TronId { get; set; }
        public int? BitcoinId { get; set; }

        [ForeignKey(nameof(TronId))]
        public virtual TblTron TblTron { get; set; }

        [ForeignKey(nameof(BitcoinId))]
        public virtual TblBitcoin TblBitcoin { get; set; }
    }
}
