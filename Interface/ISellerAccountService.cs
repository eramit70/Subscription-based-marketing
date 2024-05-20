using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.User;
using Subscription_based_marketing.Services;

namespace Subscription_based_marketing.Interface
{
    public interface ISellerAccountService
    {
        Task AddSellerAccountAsync(SellerDto SellerAccount);
        Task<bool> SellerLoginAsync(SellerDto SellerAccount);
        void SellerLogoutAsync();
        Task SellerUpdateAsync(SellerDto SellerAccount);
        Task<SellerDto> GetSellerAccountByIDAsync(Guid ID);
        //Task<List<ServiceDto>> UseSevicesListAsync(Guid SellerID);
        Task<Guid> GetSellerIDByUserNameAsync(string userName);
        Task SaveAsync();

        Task<bool> CheckDuplicateSellerAsync(string SellerUserName);
        Task UpdateLastLoginDateByUserIdAsync(Guid sellerId);
        Task DeleteSellerAccountAsync(SellerDto sellerDto);
        Task<List<SellerDto>> GetSellerAccountListAsync();


    }

}
