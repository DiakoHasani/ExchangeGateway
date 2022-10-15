using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class VinBitcoinRepository : BaseRepository<TblVinBitcoin>, IVinBitcoinRepository
    {
        public VinBitcoinRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
    public interface IVinBitcoinRepository : IBaseRepository<TblVinBitcoin>
    {
    }
}
