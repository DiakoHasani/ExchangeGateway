using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.DTO.TronScan
{
    public class GetTransferRequestModel
    {
        public int Limit { get; set; } = 20;
        public string ToAddress { get; set; }
        public string RelatedAddress { get; set; }
    }
}
