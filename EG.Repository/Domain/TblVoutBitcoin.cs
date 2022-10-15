using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblVoutBitcoin : BaseEntity
    {
        public string ScriptPubKey { get; set; }
        public string ScriptPubKeyAddress { get; set; }
        public double Value { get; set; }
        public int BitcoinId { get; set; }

        [ForeignKey(nameof(BitcoinId))]
        public virtual TblBitcoin TblBitcoin { get; set; }
    }
}
