using Microsoft.AspNetCore.Mvc;

namespace Subscription_based_marketing.Controllers
{
    public class ServiceAccessControlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
