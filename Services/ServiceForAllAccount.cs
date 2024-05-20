using Subscription_based_marketing.Interface;

namespace Subscription_based_marketing.Services
{
    public class ServiceForAllAccount : IServiceForAllAccount
    {
        private readonly IUserAccountService _userAccountService;
        private readonly ISellerAccountService _sellerAccountService;
        private readonly IAdminService _adminService;
        public ServiceForAllAccount(
            IUserAccountService userAccountService,
            ISellerAccountService sellerAccountService,
            IAdminService adminService)
        {
            _userAccountService = userAccountService;
            _sellerAccountService = sellerAccountService;

            _adminService = adminService;

        }
        public async Task<bool> CheckDuplicateUserNameInAllAccountByUserNameAsync(string userName)
        {
         bool admin =   await _adminService.CheckDuplicateAdminAsync(userName);
            bool user = await _userAccountService.CheckDuplicateUserAsync(userName);
            bool seller = await _sellerAccountService.CheckDuplicateSellerAsync(userName);

            if (admin == true || user == true || seller == true)
            {
                return false;
            }
            return true;
        }
    }
}
