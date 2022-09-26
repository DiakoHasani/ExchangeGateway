using AutoMapper;
using EG.Model.DTO.Tron;
using EG.Model.DTO.TronScan;
using EG.Model.DTO.Wallet;
using EG.Model.DTO.WebhookRequest;
using EG.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TblWallet, WalletModel>();
            CreateMap<WalletModel, TblWallet>();

            CreateMap<TblWebhookRequest, AddWebhookRequestModel>();
            CreateMap<AddWebhookRequestModel, TblWebhookRequest>();
            CreateMap<WebhookRequestModel, TblWebhookRequest>();
            CreateMap<TblWebhookRequest, WebhookRequestModel>();

            CreateMap<TokenTransferModel, TblTron>();
            CreateMap<TblTron, TokenTransferModel>();
            CreateMap<TblTron, TronModel>();
            CreateMap<TronModel, TblTron>();
        }
    }
}
