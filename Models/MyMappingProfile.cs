using AutoMapper;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Models.Adminstrator;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.ServiceControl;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.Subscription;
using Subscription_based_marketing.Models.User;


namespace Subscription_based_marketing.Models
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {

            // Maping DTO to Entity & ENtity To DTO
            CreateMap<AdminstratorAccount, AdminDto>(); 
            CreateMap<AdminDto, AdminstratorAccount>();
            CreateMap<SellerAccount, SellerDto>();
            CreateMap<SellerDto, SellerAccount>();
            CreateMap<UserAccount, UserDto>();
            CreateMap<UserDto, UserAccount>();
            CreateMap<ServiceDetail, ServiceDto>();
            CreateMap<ServiceDto, ServiceDetail>();
            CreateMap<SubscriptionDetails, SubscriptionDto>();
            CreateMap<SubscriptionDto, SubscriptionDetails>();
            CreateMap<AccessControlDto, ServiceAccessControl>();
            CreateMap<ServiceAccessControl, AccessControlDto>();
        }
    }
}
