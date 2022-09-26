using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Tron
{
    public class TronModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public long Block { get; set; }
        public string ContractAddress { get; set; }
        public string Quant { get; set; }
        public bool Confirmed { get; set; }
        public string ContractRet { get; set; }
        public string FinalResult { get; set; }
        public bool Revert { get; set; }
        public string TokenId { get; set; }
        public string TokenAbbr { get; set; }
    }
}
