using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblVinBitcoin : BaseEntity
    {
        [MaxLength(length: 200)]
        public string TransactionId { get; set; }
        public string ScriptPubkey { get; set; }
        public string ScriptPubKeyAddress { get; set; }
        public double Value { get; set; }
        public bool IsCoinbase { get; set; }
        public int BitcoinId { get; set; }

        [ForeignKey(nameof(BitcoinId))]
        public virtual TblBitcoin TblBitcoin { get; set; }
    }
}
