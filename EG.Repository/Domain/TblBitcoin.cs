using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblBitcoin : BaseEntity
    {
        [MaxLength(length: 200)]
        public string TransactionId { get; set; }

        public double Fee { get; set; }
        public bool Confirmed { get; set; }
        public int WalletId { get; set; }

        [ForeignKey(nameof(WalletId))]
        public virtual TblWallet Wallet { get; set; }
        public virtual ICollection<TblWebhookRequest> TblWebhookRequests { get; set; }
        public virtual ICollection<TblVinBitcoin> TblVinBitcoins { get; set; }
        public virtual ICollection<TblVoutBitcoin> TblVoutBitcoins { get; set; }
    }
}
