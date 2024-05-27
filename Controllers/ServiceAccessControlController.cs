using Microsoft.AspNetCore.Mvc;

namespace Subscription_based_marketing.Controllers
{
   
    public class ServiceAccessControlController : Controller
    {
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
    }
}
