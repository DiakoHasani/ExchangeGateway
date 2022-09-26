using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class TronRepository : BaseRepository<TblTron>, ITronRepository
    {
        public TronRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
    public interface ITronRepository : IBaseRepository<TblTron>
    {
    }
}
