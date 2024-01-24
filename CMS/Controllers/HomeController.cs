using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor cont;
        private readonly db_cmsContext db;


        public HomeController(ILogger<HomeController> logger , db_cmsContext db , IHttpContextAccessor cont)
        {
            _logger = logger;
            this.db = db;
            this.cont = cont;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
