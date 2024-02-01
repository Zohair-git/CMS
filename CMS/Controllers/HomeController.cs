using MailKit.Net.Smtp;
using MailKit.Security;
using CMS.Data;
using CMS.Models;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MimeKit;

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
		[HttpGet]
        public IActionResult Index()
        {
            return View(db.TblProducts.ToList());
        }
		public IActionResult ShowEvents()
		{
			TempData["Name"] = cont.HttpContext.Session.GetString("name");
			TempData["Email"] = cont.HttpContext.Session.GetString("email");
			TempData["id"] = cont.HttpContext.Session.GetInt32("session_id");

			return View(db.TblUpcomingEvents.ToList());
		}

		public IActionResult EventBookingForm(int id)
		{
			var model = db.TblUpcomingEvents.Where(x => x.Id == id).ToList();
			var usermodel = db.TblClientRegisters.Where(y => y.Name == "1").ToList();


			

			//var tbl = db.TblEventBookings.Include(a => a.User).Include(a => a.Event).ToList();

			//var sdfds = model;
			//var ffsdf = usermodel;
			//var adssd = tbl;
			return View();
		}
        [HttpGet]
		public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Login(TblClientRegister Auth)
		{
			var front_email = Auth.Email;
			var front_password = Auth.Password;

			var fetchuser = db.TblClientRegisters.Where(x => x.Email == front_email).ToList();

			var backend_name = fetchuser[0].Name;
			var backend_email = fetchuser[0].Email;
            var backend_id = fetchuser[0].Id;
			var backend_password = fetchuser[0].Password;
            if (front_email == backend_email && front_password == backend_password)
			{
				cont.HttpContext.Session.SetString("name", backend_name);
				cont.HttpContext.Session.SetString("email", backend_email);
				cont.HttpContext.Session.SetInt32("session_id", backend_id);

				var session_name = cont.HttpContext.Session.GetString("name");
				var session_email = cont.HttpContext.Session.GetString("email");
				var session_id = cont.HttpContext.Session.GetInt32("session_id");

                TempData["Name"] = session_name;
                TempData["Email"] = session_email;
                TempData["id"] = session_id;


                    return RedirectToAction("Index", "Home");

			}
			else
			{
				return RedirectToAction("ErrorPage");
			}
			return View();
		}
		public IActionResult Logout()
		{
			cont.HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}
		public IActionResult Aboutus()
        {
            return View();
        } 
        public IActionResult Contactus()
        {
            return View();
        }
        [HttpGet]
		public IActionResult Register()
		{
			return View();
		}
        [HttpPost]
		public IActionResult Register(TblClientRegister auth, string confrmpassword)
		{
			var front_email = auth.Email;
            var front_name = auth.Name;
            var msgbody = "How you doin babe";
            var pass_one = auth.Password;
            var pass_two = confrmpassword;


			
            if(pass_one == pass_two)
            {
                string value = "vwrj iivm qgpy vqdu";

                var msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("Providence Clinic", "huzaifairfan2144@gmail.com"));
                msg.To.Add(new MailboxAddress(front_name, front_email));
                msg.Subject = "Providence Clinic Account Verification";
                msg.Body = new TextPart { Text = msgbody };

                using var client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
                client.Authenticate("huzaifairfan2144@gmail.com", value);
                client.Send(msg);

				db.TblClientRegisters.Add(auth);
		        db.SaveChanges();
				return RedirectToAction("Confirmation");
			}
            else
            {
			  return RedirectToAction("Index");

            }
            return View();
		}
		[HttpGet]
		public IActionResult ProductsPage(string category)
		{
			ViewBag.category = category;
			ViewBag.AllProducts = db.TblProducts.ToList();
			ViewBag.MedicineProductsCount = db.TblProducts.Where(x=>x.Category == "medicine").Count();
			ViewBag.ScientificProductsCount = db.TblProducts.Where(x=>x.Category == "scientific").Count();
			ViewBag.AllProductsCount = db.TblProducts.Count();
			var fetchproducts = db.TblProducts.Where(x=>x.Category == category).Take(9).ToList();
			return View(fetchproducts);
		}
		public IActionResult Confirmation()
		{
			return View();
		}
		public IActionResult SubmitReview()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ProductDetails(int Id)
		{
			AllTables main_model = new AllTables()
			{
				product_list = db.TblProducts.Take(4).ToList(),
			client_register = db.TblClientRegisters.ToList(),
			upcoming_events = db.TblUpcomingEvents.ToList(),
			Images_list = db.TblImages.Where(x => x.PId == Id).ToList(),
			productss = db.TblProducts.FirstOrDefault(x => x.Id == Id),
			imagess = db.TblImages.FirstOrDefault(x => x.PId == Id),
			clientRegister = new TblClientRegister(),
			upcomingEvent = new TblUpcomingEvent(),
			bookingss = new TblEventBooking(),
			};
			return View(main_model);
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
