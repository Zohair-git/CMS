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

        public IActionResult Index()
        {
            return View();
        }
		public IActionResult ShowEvents()
		{
			return View(db.TblUpcomingEvents.ToList());
		}

		public IActionResult EventBookingForm()
		{
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
			var backend_password = fetchuser[0].Password;

			if (front_email == backend_email && front_password == backend_password)
			{
				cont.HttpContext.Session.SetString("name", backend_name);
				cont.HttpContext.Session.SetString("email", backend_email);
				cont.HttpContext.Session.SetString("password", backend_password);

				var session_name = cont.HttpContext.Session.GetString("name");
				var session_email = cont.HttpContext.Session.GetString("email");
				var session_password = cont.HttpContext.Session.GetString("password");

					return RedirectToAction("Index", "Home");

			}
			else
			{
				return RedirectToAction("ErrorPage");
			}
			return View();
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
		public IActionResult ProductsPage()
		{
			return View();
		}
		public IActionResult Confirmation()
		{
			return View();
		}
		public IActionResult ProductDetails()
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
