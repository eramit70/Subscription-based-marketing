using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Models.Subscription;

namespace Subscription_based_marketing.Interface
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionDto>> GetSubscriptionDetailsAsync();
        Task<SubscriptionDto> GetSubscriptionDetailsByUserIDAsync(Guid ID);
     
        Task AddSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task SaveAsync();
        Task SetTrueUserSubscriptionAsync(Guid ID);
        Task<Guid> SubServiceIDByUserIdAsync(Guid userId);

    }

}
