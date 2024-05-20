using AutoMapper;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.ServiceControl;
using Subscription_based_marketing.Models.Services;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;

namespace Subscription_based_marketing.Services
{
    public class ServiceAccessControll : Service<ServiceAccessControl>, IServiceAccessControll
    {
        private readonly IMapper _mapper;
        private readonly IServiceListService _serviceListService;
        private readonly IUserAccountService _userAccountService;
        private readonly ISellerAccountService _sellerAccountService;
        public ServiceAccessControll(
            ITrackableRepository<ServiceAccessControl> repository,
            IServiceListService serviceListService,
            IUserAccountService userAccountService,
            ISellerAccountService sellerAccountService,

            IMapper mapper):base(repository)
        {
            _serviceListService = serviceListService;
            _userAccountService = userAccountService;
            _sellerAccountService = sellerAccountService;
            _mapper = mapper;
        }

        public async Task CreateServiceAccessControl(AccessControlDto controlDto)
        {
            Guid Id = Guid.NewGuid();
            controlDto.ServiceAccessControlID = Id;
            controlDto.serviceID = Id;
            controlDto.UserId = Id;
            controlDto.AccessStartTime = DateTime.Now;
            controlDto.AccessEndTime = DateTime.Now;
           var serviceControl =  _mapper.Map<ServiceAccessControl>(controlDto);
        }
    }
}
