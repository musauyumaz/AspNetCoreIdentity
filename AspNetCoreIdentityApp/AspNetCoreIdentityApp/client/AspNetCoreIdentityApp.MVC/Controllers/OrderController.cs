using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.MVC.Controllers
{
    public class OrderController : Controller
    {
        [Authorize("OrderPermissionReadOrDelete")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
