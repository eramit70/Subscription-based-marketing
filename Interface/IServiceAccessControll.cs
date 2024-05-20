using Subscription_based_marketing.DTO;

namespace Subscription_based_marketing.Interface
{
    public interface IServiceAccessControll
    {
      public Task CreateServiceAccessControl(AccessControlDto controlDto);
    }
}
