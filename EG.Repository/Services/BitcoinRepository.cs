using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class BitcoinRepository:BaseRepository<TblBitcoin>, IBitcoinRepository
    {
        public BitcoinRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
    public interface IBitcoinRepository:IBaseRepository<TblBitcoin>
    {
    }
}
