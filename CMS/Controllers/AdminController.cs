using CMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View();
        }
        [HttpPost]
		public IActionResult ProductAdd(TblImage image , IFormFile imgs)
		{
            foreach (var  item in image)
            {
                
            }
            var dfd = "dfsd";
			return View();
		}
		public IActionResult ProductView()
        {
            return View();
        }
    }
}
