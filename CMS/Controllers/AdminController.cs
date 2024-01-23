using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
