using EG.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.Services
{
    public class ErrorBusiness : IErrorBusiness
    {
        private readonly IErrorRepository _errorRepository;
        public ErrorBusiness(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public void AddError(Exception ex, string address)
        {
            try
            {
                _errorRepository.Add(new Repository.Domain.TblError
                {
                    InnerException = ex.InnerException != null ? ex.InnerException.Message ?? "" : "",
                    Message = ex.Message ?? "",
                    StackTrace = ex.StackTrace ?? "",
                    Address = address
                });
                _errorRepository.SaveChange();
            }
            catch(Exception)
            {

            }
        }

        public void AddError(string message, string address)
        {
            try
            {
                _errorRepository.Add(new Repository.Domain.TblError
                {
                    InnerException = "",
                    Message = message,
                    StackTrace = "",
                    Address = address
                });
                _errorRepository.SaveChange();
            }
            catch(Exception)
            {

            }
        }
    }
    public interface IErrorBusiness
    {
        void AddError(Exception ex, string address);
        void AddError(string message, string address);
    }
}
