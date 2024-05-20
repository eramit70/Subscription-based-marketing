using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.Models.User;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.DTO;

namespace Subscription_based_marketing.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserAccountService _userService;
        private readonly IServiceForAllAccount _serviceForAll;

        public UserAccountController(
            IUserAccountService userService,
            IServiceForAllAccount serviceForAll
          )
        {
            _userService = userService;

            _serviceForAll = serviceForAll;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                bool checkAllAccount = await _serviceForAll.CheckDuplicateUserNameInAllAccountByUserNameAsync(userDto.UserName);
                bool duplicateUserit = await _userService.CheckDuplicateUserAsync(userDto.UserName);
                if (!duplicateUserit)
                {
                    if (checkAllAccount)
                    {
                        await _userService.AddUserAccountAsync(userDto);
                        TempData["Registered"] = "Congrats! " + userDto.FirstName;
                        HttpContext.Session.SetString("UserLogin", userDto.UserName);

                        return RedirectToAction("Login", "UserAccount");
                    }
                    else
                    {
                        TempData["duplicateAccount"] = "Using "+userDto.UserName.ToUpper() + " UserName Already Create Account in other Service ";
                        return View();
                    }
                    }
                    else
                    {
                        TempData["duplicate"] = userDto.UserName.ToUpper() + " Already Registered ";
                        return View();
                    }

                }

                return View(userDto);
            }

            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login(UserDto userDto)
            {
                bool check = await _userService.UserLoginAsync(userDto);
                if (check)
                {
                    HttpContext.Session.SetString("UserLogin", userDto.UserName);
                    bool status = await _userService.FIndSubscriptionStatusAsync(userDto.UserName);

                    if (status)
                    {
                        return RedirectToAction("PremiumDashBoard", "UserDashboard");
                    }
                    else
                    {
                        return RedirectToAction("NormalDashBoard", "UserDashboard");
                    }
                }
                else
                {
                    TempData["LoginError"] = "Entered Details Don't Match";
                    return View();
                }
            }

            public async Task<IActionResult> Update(Guid ID)
            {
                var userLogin = HttpContext.Session.GetString("UserLogin");

                if (!string.IsNullOrEmpty(userLogin))
                {

                    var userDto = await _userService.GetUserByIdAsync(ID);
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
            public async Task<IActionResult> Update(UserDto userDto)
            {

                await _userService.UserUpdateAsync(userDto);
                return RedirectToAction("PremiumDashBoard", "UserDashboard");

            }
            public async Task<IActionResult> Details()
            {
                var userLogin = HttpContext.Session.GetString("UserLogin");

                if (!string.IsNullOrEmpty(userLogin))
                {
                    Guid userId = await _userService.GetIDByUserNameAsync(userLogin);
                    var userDto = await _userService.GetUserByIdAsync(userId);
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

            public async Task<IActionResult> UserLogout()
            {
                var userUserName = HttpContext.Session.GetString("UserLogin");
                Guid userId = await _userService.GetIDByUserNameAsync(userUserName);

                await _userService.UpdateLastLoginDateByUserIdAsync(userId);
                HttpContext.Session.Remove("UserLogin");

                return RedirectToAction("Login", "UserAccount");
            }
        }
    }
