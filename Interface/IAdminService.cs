using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Models.Adminstrator;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.User;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Trackable;


namespace Subscription_based_marketing.Interface
{
    public interface IAdminService 

    {
        Task AddAdminAccountAsync(AdminDto admin);
        Task<bool> AdminLoginAsync(AdminDto admin);
        void AdminLogoutAsync();
        Task AdminUpdateAsync(AdminDto admin);
        Task SaveAsync();
       
        Task<AdminDto> GetAdminstratorByIdAsync(Guid ID);
        Task<bool> CheckDuplicateAdminAsync(string AdminuserName);
        Task<Guid> GetAdminIDByUserNameAsync(string adminUserName);
        Task UpdateLastLoginDateByUserIdAsync(Guid adminId);

    }

}
