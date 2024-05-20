using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Trackable;
using URF.Core.Services;

namespace Subscription_based_marketing.Services
{
    public class ServiceListService : Service<ServiceDetail>, IServiceListService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceListService(
            ITrackableRepository<ServiceDetail> repository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateServiceAsync(ServiceDto serviceDto)
        {

            var serviceDetail = _mapper.Map<ServiceDetail>(serviceDto);

            Repository.Insert(serviceDetail);
            await SaveChangeAsync();
        }

        public async Task DeleteServiceAsync(ServiceDto serviceDto)
        {
            var serviceDetail = _mapper.Map<ServiceDetail>(serviceDto);
             Repository.Delete(serviceDetail);
            await SaveChangeAsync();
        }
        public async Task<List<ServiceDto>> GetAllServiceListAsync()
        {
            var serviceDetails = await Repository.Queryable().Where(item => item.ServiceIsPublish == true).ToListAsync();
            return _mapper.Map<List<ServiceDto>>(serviceDetails);
        }

        public async Task<ServiceDto> GetDetailsServiceAsync(Guid ID)
        {
            var serviceDetail = await Repository.Queryable().
                Where(item => item.ServiceID == ID).FirstOrDefaultAsync();
            return _mapper.Map<ServiceDto>(serviceDetail);
        }


        public async Task<List<ServiceDto>> GetServiceListBySeller(ServiceDto serviceDto)
        {
            var serviceDetail = _mapper.Map<ServiceDetail>(serviceDto);
            var serviceDetails = await Repository.Queryable()
                .Where(item => item.SellerID == serviceDetail.SellerID).ToListAsync();
            return _mapper.Map<List<ServiceDto>>(serviceDetails);
        }

        public async Task SaveChangeAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateServiceAsync(ServiceDto serviceDto)
        {
            var serviceDetail = _mapper.Map<ServiceDetail>(serviceDto);
            Repository.Update(serviceDetail);
            await SaveChangeAsync();
        }

        public async Task<Duration> GetDurationByServiceIDAsync(Guid ID)
        {
            var service = await Repository.Queryable().
                Where(item => item.ServiceID == ID).FirstOrDefaultAsync();

            return service.ServiceDuration;

        }

        public async Task<ServiceDto> GetServiceDetailsBySubServiceIDAsync(Guid SubServiceId)
        {
            var serviceDetails = await Repository.Queryable().
                Where(item => item.ServiceID == SubServiceId).FirstOrDefaultAsync();

            return _mapper.Map<ServiceDto>(serviceDetails);

        }

        public async Task<List<ServiceDto>> UseSevicesListAsync(Guid sellerID)
        {
            var serviceDetails = await Repository.Queryable().
                Where(item => item.SellerID ==  sellerID).ToListAsync();
      
            var serviceDetailDtos = _mapper.Map<List<ServiceDto>>(serviceDetails);

            return serviceDetailDtos;
        }

    }


}
