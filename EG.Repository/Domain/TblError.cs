using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class TblError : BaseEntity
    {
        public string Message { get; set; }

        public string InnerException { get; set; }

        public string Address { get; set; }

        public string StackTrace { get; set; }
    }
}
