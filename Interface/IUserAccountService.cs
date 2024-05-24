using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.User;

namespace Subscription_based_marketing.Interface
{
    public interface IUserAccountService
    {
        Task AddUserAccountAsync(UserDto userDto);
        Task<bool> UserLoginAsync(UserDto userDto);
        Task<bool> FIndSubscriptionStatusAsync(string userName);
        Task UserUpdateAsync(UserDto userDto);
        Task SaveAsync();
        Task<UserDto> GetUserByIdAsync(Guid Id);
        Task<bool> CheckDuplicateUserAsync(string userName);
        Task<Guid> GetIDByUserNameAsync(string userName);
        Task SetTrueUserSubscriptionAsync(Guid ID);
        Task UpdateLastLoginDateByUserIdAsync(Guid userId);
        Task DeleteUserAccountAsync(UserDto userDto);
        Task<List<UserDto>> GetUserAccountListAsync();
        Task<bool> CheckDuplicateEmailAsync(string email);
    }

}
