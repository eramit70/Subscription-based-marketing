using Microsoft.AspNetCore.Mvc;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;

namespace Subscription_based_marketing.Controllers
{

    public class AdminAccountController : Controller
    {
        #region Service Inject
        private readonly IAdminService _adminService;
        private readonly IServiceListService _serviceList;
        private readonly IUserAccountService _userAccountService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISellerAccountService _sellerService;
        private readonly IServiceForAllAccount _allacountService;


        public AdminAccountController(
            IAdminService adminService,
            IServiceListService serviceList,
            IUserAccountService userAccountService,
            ISubscriptionService subscriptionService,
            ISellerAccountService sellerAccount,
            IServiceForAllAccount serviceForAllAccount
           )
        {
            _adminService = adminService;
            _serviceList = serviceList;
            _userAccountService = userAccountService;
            _sellerService = sellerAccount;
            _allacountService = serviceForAllAccount;
            _subscriptionService = subscriptionService;

        }
        #endregion


        #region Admin Action 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminDto admin)
        {

            bool loginResult = await _adminService.AdminLoginAsync(admin);
            if (loginResult)
            {
                HttpContext.Session.SetString("AdminLogin", admin.AdminUserName);
                return RedirectToAction("Index", "AdminAccount"); // Redirect to home page or dashboard
            }
            else
            {
                TempData["LoginError"] = "Entered data doesn't match. Please try again.";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AdminDto admin)
        {
            if (ModelState.IsValid)
            {
                bool duplicateEmailGlobally = await _allacountService.CheckDuplicateEmailAllAccountByEmailAsync(admin.AdminEmail);
                bool duplicateUserNameGlobaly = await _allacountService.CheckDuplicateUserNameInAllAccountByUserNameAsync(admin.AdminUserName);
                bool duplicateUserName = await _adminService.CheckDuplicateAdminAsync(admin.AdminUserName);
                bool duplicateEmailAddress = await _adminService.CheckDuplicateAdminAsync(admin.AdminEmail);

                if (!duplicateEmailAddress)
                {
                    if (!duplicateUserName)
                    {
                        if (duplicateUserNameGlobaly)
                        {
                            if (duplicateEmailGlobally)
                            {
                                 await _adminService.AddAdminAccountAsync(admin);
                                TempData["Registered"] = "Congratulations! " + admin.FirstName.ToUpper() + " your account has been registered.";

                                return RedirectToAction("Login");
                            }
                            else
                            {
                                TempData["duplicateAccount"] = "Using " + admin.AdminEmail + "  Email Address Already Registered in other Service ";
                                return View();
                            }
                        }
                        else
                        {
                            TempData["duplicateAccount"] = "Using " + admin.AdminUserName.ToUpper() + "  UserName Already Create Account in other Service ";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["duplicate"] = admin.AdminUserName.ToUpper() + " Already Registered ";
                        return View();
                    }
                }
                else
                {
                    TempData["duplicate"] = admin.AdminEmail + " Already Registered ";
                    return View();
                }
            }
            else
            {
                return View(admin);
            }
        }

        public async Task<IActionResult> AdminLogout()
        {

            var adminUserName = HttpContext.Session.GetString("AdminLogin");
            Guid adminId = await _adminService.GetAdminIDByUserNameAsync(adminUserName);

            await _adminService.UpdateLastLoginDateByUserIdAsync(adminId);
            HttpContext.Session.Remove("AdminLogin");

            return RedirectToAction("Login", "AdminAccount");
        }

        #endregion  

        #region Service Action
        public async Task<IActionResult> ServiceList()
        {
            if (HttpContext.Session.GetString("AdminLogin") != null)
            {
                var name = HttpContext.Session.GetString("AdminLogin");
                var data = await _serviceList.GetAllServiceListAsync();

                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }
        }

        #endregion


        #region Seller Account Action
        public async Task<IActionResult> SellerAccountList()
        {
            if (HttpContext.Session.GetString("AdminLogin") != null)
            {
                var name = HttpContext.Session.GetString("AdminLogin");
                var data = await _sellerService.GetSellerAccountListAsync();

                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }
        }
        public async Task<IActionResult> DeleteSellerAccount(Guid sellerId)
        {
            var sellerDto = await _sellerService.GetSellerAccountByIDAsync(sellerId);
            return View(sellerDto);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteSellerAccount(SellerDto sellerDto)
        {
            try
            {
                await _sellerService.DeleteSellerAccountAsync(sellerDto);
            }
            catch
            {
                TempData["DeleteError"] = " Sorry! We can't Delete " + sellerDto.SellerUserName.ToUpper() + " Account !!";
            }
            return RedirectToAction("SellerAccountList", "AdminAccount");
        }


        public async Task<IActionResult> UploadedServicesBySeller(Guid sellerId)
        {
            var seller = await _sellerService.GetSellerAccountByIDAsync(sellerId);
            try
            {
                List<ServiceDto> serviceList = await _serviceList.UseSevicesListAsync(sellerId);

                return View(serviceList);

            }
            catch
            {
                TempData["NoService"] = seller.SellerName + " No service available";
                return RedirectToAction("UserList", "AdminAccount");
            }
        }

        #endregion


        #region User Account Action

        public async Task<IActionResult> UsedServiceByUser(Guid userId)
        {
            var user = await _userAccountService.GetUserByIdAsync(userId);
            try
            {
                var serviceId = await _subscriptionService.SubServiceIDByUserIdAsync(userId);
                var serviceDetail = await _serviceList.GetServiceDetailsBySubServiceIDAsync(serviceId);
                return View(serviceDetail);

            }
            catch
            {
                TempData["NoService"] = user.UserName + "  No service available";
                return RedirectToAction("UserList", "AdminAccount");
            }
        }

        public async Task<IActionResult> UserList()
        {
            if (HttpContext.Session.GetString("AdminLogin") != null)
            {
                var name = HttpContext.Session.GetString("AdminLogin");
                var data = await _userAccountService.GetUserAccountListAsync();

                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");
            }
        }



        public async Task<IActionResult> DeleteUserAccount(Guid userId)
        {
            UserDto userDto = await _userAccountService.GetUserByIdAsync(userId);

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserAccount(UserDto userDto)
        {
            try
            {
                await _userAccountService.DeleteUserAccountAsync(userDto);
            }
            catch
            {
                TempData["DeleteError"] = "Sorry! We can't Delete " + userDto.UserName.ToUpper() + " Account !!";
            }

            return RedirectToAction("UserList", "AdminAccount");

        }

        #endregion


    }
}
