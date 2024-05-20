using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;

namespace Subscription_based_marketing.Controllers
{
    public class SellerAccountController : Controller
    {
        #region Dependency Injection

        private readonly ISellerAccountService _sellerService;
        private readonly IServiceForAllAccount _serviceForAll;

        private readonly IMapper _mapper;
        public SellerAccountController(
            ISellerAccountService sellerService,
            IServiceForAllAccount serviceForAll,
            IMapper mapper)
        {
            _sellerService = sellerService;
            _serviceForAll = serviceForAll;
            _mapper = mapper;
        }


        #endregion

        public IActionResult Login()
        {
            return View();
        }

        #region Login / Register

        [HttpPost]
        public IActionResult Login(SellerDto sellerDto)
        {
            bool check = _sellerService.SellerLoginAsync(sellerDto).Result;
            if (check)
            {
                HttpContext.Session.SetString("SellerUserName", sellerDto.SellerUserName);

                return RedirectToAction("ServiceList", "ServiceDetails");
            }
            else
            {
                TempData["LoginError"] = "Entered details are invalid.";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(SellerDto sellerDto)
        {
            if (!ModelState.IsValid)
            {
                bool checkAllAccount = await _serviceForAll.CheckDuplicateUserNameInAllAccountByUserNameAsync(sellerDto.SellerUserName);



                bool duplicate = await _sellerService.CheckDuplicateSellerAsync(sellerDto.SellerUserName);
                if (!duplicate)
                {
                    if (checkAllAccount)
                    {

                        await _sellerService.AddSellerAccountAsync(sellerDto);
                        TempData["Registered"] = "Congratulations! " + sellerDto.SellerName.ToUpper() + " your account has been registered.";

                        return RedirectToAction("Login", "SellerAccount");
                    }
                    else
                    {
                        TempData["duplicateAccount"] ="Using "+ sellerDto.SellerUserName.ToUpper() + " UserName Already Create Account in other Service ";
                        return View();
                    }
                }
                else
                {
                    TempData["duplicate"] = sellerDto.SellerName.ToUpper() + " Is Already Registered";
                    return View();
                }
            }
            else
            {
                return View(sellerDto);
            }
        }

        #endregion

        #region Update 
        public async Task<IActionResult> Update(Guid ID)
        {
            var sellerAccount = await _sellerService.GetSellerAccountByIDAsync(ID);


            return View(sellerAccount);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SellerDto sellerDto)
        {

            await _sellerService.SellerUpdateAsync(sellerDto);
            return RedirectToAction("Details", "SellerAccount");


        }

        #endregion


        #region Details
        public async Task<IActionResult> Details()
        {
            if (HttpContext.Session.GetString("SellerUserName") != null)
            {
                var sellerUserName = HttpContext.Session.GetString("SellerUserName");
                Guid sellerId = await _sellerService.GetSellerIDByUserNameAsync(sellerUserName);

                var sellerAccount = await _sellerService.GetSellerAccountByIDAsync(sellerId);

                return View(sellerAccount);
            }
            else
            {
                return RedirectToAction("Login", "SellerAccount");
            }
        }


        /*  public async Task<IActionResult> Logout()
          {
              var sellerUserName = HttpContext.Session.GetString("SellerUserName");
              Guid sellerId = await _sellerService.GetSellerIDByUserNameAsync(sellerUserName);

              await _sellerService.UpdateLastLoginDateByUserIdAsync(sellerId);
              HttpContext.Session.Remove("SellerUserName");

              return RedirectToActionPermanent("Login", "SellerAccount");
          }*/

        public async Task<IActionResult> SellerLogout()
        {
            var sellerUserName = HttpContext.Session.GetString("SellerUserName");
            Guid sellerId = await _sellerService.GetSellerIDByUserNameAsync(sellerUserName);

            await _sellerService.UpdateLastLoginDateByUserIdAsync(sellerId);
            HttpContext.Session.Remove("SellerUserName");

            return RedirectToActionPermanent("Login", "SellerAccount");


        }

        #endregion
    }
}
