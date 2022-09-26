using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
