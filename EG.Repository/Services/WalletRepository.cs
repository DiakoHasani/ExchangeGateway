using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class WalletRepository : BaseRepository<TblWallet>, IWalletRepository
    {
        public WalletRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
    public interface IWalletRepository : IBaseRepository<TblWallet>
    {
    }
}
