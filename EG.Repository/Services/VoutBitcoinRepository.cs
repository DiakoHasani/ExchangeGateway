using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class VoutBitcoinRepository : BaseRepository<TblVoutBitcoin>, IVoutBitcoinRepository
    {
        public VoutBitcoinRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
    public interface IVoutBitcoinRepository : IBaseRepository<TblVoutBitcoin>
    {
    }
}
