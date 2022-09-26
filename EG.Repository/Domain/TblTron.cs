using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblTron : BaseEntity
    {
        [MaxLength(length: 200)]
        public string TransactionId { get; set; }

        [MaxLength(length: 100)]
        public string FromAddress { get; set; }

        [MaxLength(length: 100)]
        public string ToAddress { get; set; }

        public long Block { get; set; }

        [MaxLength(length: 200)]
        public string ContractAddress { get; set; }

        [MaxLength(length: 60)]
        public string Quant { get; set; }

        public bool Confirmed { get; set; }

        [MaxLength(length: 20)]
        public string ContractRet { get; set; }

        [MaxLength(length: 20)]
        public string FinalResult { get; set; }

        public bool Revert { get; set; }

        [MaxLength(length: 100)]
        public string TokenId { get; set; }

        [MaxLength(length:20)]
        public string TokenAbbr { get; set; }

        /// <summary>
        /// اگر مقدار زیر نال باشد یعنی این ارز کوین است در غیر این صورت یعنی ارز از نوع توکن است
        /// </summary>
        public TokenTypeEnum? TokenType { get; set; }
        public int WalletId { get; set; }

        [ForeignKey(nameof(WalletId))]
        public virtual TblWallet TblWallet { get; set; }

        public virtual ICollection<TblWebhookRequest> TblWebhookRequests { get; set; }
    }
}
