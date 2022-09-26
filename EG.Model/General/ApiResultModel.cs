using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.General
{
    public class ApiResultModel<T>
    {
        public T Response { get; set; }
        public bool Result { get; set; } = false;
        public List<string> Messages { get; set; } = new List<string>();
    }
}
