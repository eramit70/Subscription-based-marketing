using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.Subscription;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Trackable;
using URF.Core.Services;

namespace Subscription_based_marketing.Services
{
    public class SubscriptionService :Service<SubscriptionDetails>, ISubscriptionService
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(
            ITrackableRepository<SubscriptionDetails> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper) :base(repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddSubscriptionAsync(SubscriptionDto subscriptionDto)
        {


            var subscription = _mapper.Map<SubscriptionDetails>(subscriptionDto);


             Repository.Insert(subscription);

            await SaveAsync();
        }


        public async Task SetTrueUserSubscriptionAsync(Guid ID)
        {
            var userAccount = await Repository.Queryable().
                Where(item => item.SubscriptionID == ID).FirstOrDefaultAsync();
            if (userAccount != null)
            {
                userAccount.SubscriptionStatus = Enums.Status.Active;
                Repository.Update(userAccount);
                await SaveAsync();
            }
        }



        public async Task<List<SubscriptionDto>> GetSubscriptionDetailsAsync()
        {
            var subscriptionDetails = await Repository.Queryable().ToListAsync();
            return _mapper.Map<List<SubscriptionDto>>(subscriptionDetails);
        }

        /* public async Task<SubscriptionDto> GetSubscriptionDetailsByIDAsync(Guid ID)
         {
             var subscription = await Repository.SubscriptionDetails.FirstOrDefaultAsync(s => s.SubscriptionID == ID);
             return _mapper.Map<SubscriptionDto>(subscription);
         }*/

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

 

        public async Task<SubscriptionDto> GetSubscriptionDetailsByUserIDAsync(Guid UserID)
        {
            var subscription = await Repository.Queryable().
                Where(item => item.UserID == UserID).FirstOrDefaultAsync();
            return _mapper.Map<SubscriptionDto>(subscription);
        }

       

        public async Task<Guid> SubServiceIDByUserIdAsync(Guid userID)
        {
            try
            {
            var service = await Repository.Queryable().
                Where(item => item.UserID == userID).FirstOrDefaultAsync();

                if (service != null)
                {

                    return service.serviceID;
                }
                return userID;
            }
            catch (Exception ex)
            {

                return userID;
            }
        }




    }
}
