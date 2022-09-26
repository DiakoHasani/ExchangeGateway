using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblSellRequest : BaseEntity
    {
        public Guid TransactionKey { get; set; }
        public double Amount { get; set; }
        public CoinTypeEnum CoinType { get; set; }
        public SellRequestStatusEnum Status { get; set; }
        public int WalletId { get; set; }

        [ForeignKey(nameof(WalletId))]
        public virtual TblWallet TblWallet { get; set; }

    }
}
