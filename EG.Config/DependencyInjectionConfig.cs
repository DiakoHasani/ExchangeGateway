using EG.Business.Apis;
using EG.Business.Services;
using EG.Repository.Domain;
using EG.Repository.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Config
{
    public class DependencyInjectionConfig
    {
        public void Config(IServiceCollection services)
        {
            SetRepository(services);
            SetApi(services);
            SetBusiness(services);
        }

        private void SetRepository(IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWebhookRequestRepository, WebhookRequestRepository>();
            services.AddScoped<ITronRepository, TronRepository>();
            services.AddScoped<IBitcoinRepository, BitcoinRepository>();
        }

        private void SetApi(IServiceCollection services)
        {
            services.AddScoped<ITronScanApi, TronScanApi>();
            services.AddScoped<IWebhookApi, WebhookApi>();
        }

        private void SetBusiness(IServiceCollection services)
        {
            services.AddScoped<IErrorBusiness, ErrorBusiness>();
            services.AddScoped<IWalletBusiness, WalletBusiness>();
            services.AddScoped<ITronBusiness, TronBusiness>();
            services.AddScoped<IWebhookRequestBusiness, WebhookRequestBusiness>();
            services.AddScoped<IBitcoinBusiness, BitcoinBusiness>();
        }
    }
}
