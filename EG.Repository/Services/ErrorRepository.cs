using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class ErrorRepository : BaseRepository<TblError>, IErrorRepository
    {
        public ErrorRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
    public interface IErrorRepository : IBaseRepository<TblError>
    {
    }
}
