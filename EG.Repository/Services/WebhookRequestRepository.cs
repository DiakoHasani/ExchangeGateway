using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Repository.Services
{
    public class WebhookRequestRepository : BaseRepository<TblWebhookRequest>, IWebhookRequestRepository
    {
        private readonly DataContext _dataContext;
        public WebhookRequestRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddRange(List<TblWebhookRequest> webhookRequests)
        {
            _dataContext.TblWebhookRequests.AddRange(webhookRequests);
        }
    }
    public interface IWebhookRequestRepository : IBaseRepository<TblWebhookRequest>
    {
        void AddRange(List<TblWebhookRequest> webhookRequests);
    }
}
