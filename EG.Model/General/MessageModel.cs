using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Model.General
{
    public class MessageModel<T>
    {
        public bool Result { get; set; } = false;
        public List<string> Messages { get; set; } = new List<string>();
        public T Data { get; set; }
    }
    public class MessageModel
    {
        public bool Result { get; set; } = false;
        public List<string> Messages { get; set; } = new List<string>();
    }
}
