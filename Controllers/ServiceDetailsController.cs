using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;

namespace Subscription_based_marketing.Controllers
{
        public class ServiceDetailsController : Controller
    {
        private readonly IServiceListService _serviceList;
        private readonly ISellerAccountService _sellerService;
        private readonly IMapper _mapper;
        public ServiceDetailsController(
            IServiceListService serviceListService,
            IMapper mapper,
            ISellerAccountService sellerAccountService
            )
        {
            _serviceList = serviceListService;
            _mapper = mapper;
            _sellerService = sellerAccountService;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ServiceList()
        {
            if (HttpContext.Session.GetString("SellerUserName") != null)
            {
                var name = HttpContext.Session.GetString("SellerUserName");
                Guid SellerID =   _sellerService.GetSellerIDByUserNameAsync(name!).Result;
                var serviceDtoList =  _serviceList.UseSevicesListAsync(SellerID).Result;
              
                return View(serviceDtoList);
            }
            else
            {
                return RedirectToAction("Login", "SellerAccount");
            }
        }
        [HttpGet]
        public IActionResult CreateService()
        {
            if (HttpContext.Session.GetString("SellerUserName") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "SellerAccount");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                var name = HttpContext.Session.GetString("SellerUserName").ToString();
                Guid ID = Guid.NewGuid();
                serviceDto.ServiceID = ID;
                serviceDto.SellerID = await _sellerService.GetSellerIDByUserNameAsync(name);
                serviceDto.ServiceCreationDate = DateTime.Now;

                await _serviceList.CreateServiceAsync(serviceDto);
                TempData["AddedService"] = "Service added successfully!";

                return RedirectToAction("ServiceList");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailService(Guid ID)
        {
            if (HttpContext.Session.GetString("SellerUserName") != null)
            {
                var serviceDto = await _serviceList.GetDetailsServiceAsync(ID);
               

                return View(serviceDto);
            }
            else
            {
                return RedirectToAction("Login", "SellerAccount");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(Guid ID)
        {
            if (HttpContext.Session.GetString("SellerUserName") != null)
            {
                var serviceDto = await _serviceList.GetDetailsServiceAsync(ID);
                return View(serviceDto);
            }
            else
            {
                return RedirectToAction("Login", "SellerAccount");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(ServiceDto serviceDto)
        {

            if (!ModelState.IsValid)
            {
                await _serviceList.UpdateServiceAsync(serviceDto);
                TempData["UpdateService"] = "Service has been updated";

                return RedirectToAction("ServiceList", "ServiceDetails");
            }
            else
            {
                return View(serviceDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteService(Guid ID)
        {
            if (HttpContext.Session.GetString("SellerUserName") != null)
            {
                var serviceDto = await _serviceList.GetDetailsServiceAsync(ID);
               

                return View(serviceDto);
            }
            else
            {
                return RedirectToAction("Login", "SellerAccount");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(ServiceDto serviceDto)
        {
            try
            {
                await _serviceList.DeleteServiceAsync(serviceDto);
                TempData["DeleteService"] = "Service has been deleted!";
            }
            catch
            {
                TempData["DeleteError"] = "We can't Delete " + serviceDto.ServiceTitle.ToUpper() + " Service !!";
            }
            return RedirectToAction("ServiceList");
        }

    }
}
