using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductAdd()
        {
            return View();
        }
        public IActionResult ProductView()
        {
            return View();
        }
    }
}
