using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.Subscription;
using Subscription_based_marketing.Models.User;
using URF.Core.Services;

namespace Subscription_based_marketing.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly IServiceListService _serviceList;
        private readonly IUserAccountService _userAccountService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public UserDashboardController(
            IServiceListService serviceList,
            IUserAccountService userAccountService,
            ISubscriptionService subscriptionService,
            IMapper mapper)
        {
            _serviceList = serviceList;
            _userAccountService = userAccountService;
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> SubscriptionList()
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrEmpty(userLogin))
            {
                var serviceListDto = await _serviceList.GetAllServiceListAsync();
             
                return View(serviceListDto);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }

        public async Task<IActionResult> PaymentProcess(Guid ID)
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrEmpty(userLogin))
            {
                var serviceDto = await _serviceList.GetDetailsServiceAsync(ID);
               
                return View(serviceDto);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }
        public async Task<IActionResult> NormalDashBoard(UserDto userAccount)
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrEmpty(userLogin))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }



        /*    [HttpPost]
            public async Task<IActionResult> BuySubscription(SubscriptionDto subscriptionDto)
            {
                var userLogin = HttpContext.Session.GetString("UserLogin");
                if (!string.IsNullOrEmpty(userLogin))
                {
                    if (ModelState.IsValid)
                    {
                        var subscription = _mapper.Map<SubscriptionDto>(subscriptionDto);
                        subscription.SubscriptionStartDate = DateTime.Now;
                        subscription.SubscriptionNextBillingDate = DateTime.Now;
                        subscription.SubscriptionStatus = Enums.Status.Active;

                        await _subscriptionService.AddSubscriptionAsync(subscription);
                        TempData["SubscriptionMessage"] = "Subscription purchased successfully!";
                        return RedirectToAction("SubscriptionList");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "UserAccount");
                }

                return View(subscriptionDto);
            }*/

        public async Task<IActionResult> PaymentSuccess(Guid ID)
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrEmpty(userLogin))
            {
                var serviceDto = await _serviceList.GetDetailsServiceAsync(ID);
              
                return View(serviceDto);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSuccess(ServiceDto serviceDto)
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
            if (!string.IsNullOrEmpty(userLogin))
            {
                
                var duration = await _serviceList.GetDurationByServiceIDAsync(serviceDto.ServiceID);
                Guid userID = await _userAccountService.GetIDByUserNameAsync(userLogin);

               
                await _userAccountService.SetTrueUserSubscriptionAsync(userID);
               
                SubscriptionDto subscriptionDto = new SubscriptionDto();


                Guid subscriptionID = Guid.NewGuid();
                subscriptionDto.SubscriptionID = subscriptionID;
                subscriptionDto.serviceID = serviceDto.ServiceID;
                subscriptionDto.UserID = userID;
                
                subscriptionDto.SubscriptionStartDate = DateTime.Now;
                if (duration == Enums.Duration.Year)
                {
                    subscriptionDto.SubscriptionNextBillingDate = subscriptionDto.SubscriptionStartDate.AddYears(1);
                    subscriptionDto.SubscriptionBillingFrequency = Enums.BillingFrequency.year;

                }
                else if (duration == Enums.Duration.Month)
                {
                    subscriptionDto.SubscriptionNextBillingDate = subscriptionDto.SubscriptionStartDate.AddMonths(1);
                    subscriptionDto.SubscriptionBillingFrequency = Enums.BillingFrequency.month;

                }
                else if (duration == Enums.Duration.Week)
                {
                    subscriptionDto.SubscriptionNextBillingDate = subscriptionDto.SubscriptionStartDate.AddDays(7);
                    subscriptionDto.SubscriptionBillingFrequency = Enums.BillingFrequency.week;

                }
                subscriptionDto.SubscriptionStatus = Enums.Status.Active;

                await _subscriptionService.AddSubscriptionAsync(subscriptionDto);
                TempData["SubscriptionMessage"] = "Subscription purchased successfully!";
                return RedirectToAction("PremiumDashBoard", "UserDashboard");
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }





        // ---------------------------------------------------Premium DashBoard Action Method----------------------------------



        public async Task<IActionResult> PremiumDashBoard(UserDto userAccount)
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
          Guid userId =   await _userAccountService.GetIDByUserNameAsync(userLogin);
          
            if (!string.IsNullOrEmpty(userLogin))
            {
                var userDto = await _subscriptionService.GetSubscriptionDetailsByUserIDAsync(userId);

                return View(userDto);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }
        public async Task<IActionResult> ViewSerives()
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");
            Guid userId = await _userAccountService.GetIDByUserNameAsync(userLogin);
            Guid serviceId = await _subscriptionService.SubServiceIDByUserIdAsync(userId);

            if (!string.IsNullOrEmpty(userLogin))
            {

                var serviceDetail = await _serviceList.GetServiceDetailsBySubServiceIDAsync(serviceId);
             
                return View(serviceDetail);
            }

            else
            {
                return RedirectToAction("Login", "UserAccount");
            }


        }

        // ---------------------------------------------------Premium Use Account Management Action Method----------------------------------


        public async Task<IActionResult> UserUpdate(Guid ID)
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");

            if (!string.IsNullOrEmpty(userLogin))
            {

                var userDto = await _userAccountService.GetUserByIdAsync(ID);
                if (userDto == null)
                {
                    return NotFound();
                }

                return View(userDto);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserDto userDto)
        {

            await _userAccountService.UserUpdateAsync(userDto);
            return RedirectToAction("PremiumDashBoard", "UserDashboard");

        }
        public async Task<IActionResult> UserDetails()
        {
            var userLogin = HttpContext.Session.GetString("UserLogin");

            if (!string.IsNullOrEmpty(userLogin))
            {
                Guid userId = await _userAccountService.GetIDByUserNameAsync(userLogin);
                var userDto = await _userAccountService.GetUserByIdAsync(userId);
                if (userDto == null)
                {
                    return NotFound();
                }

                return View(userDto);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
        }
    }
}
