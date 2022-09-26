using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.Wallet
{
    public class UpdateTransactionIdBodyModel
    {
        public int WalletId { get; set; }
        public string Txid { get; set; }
    }
}
