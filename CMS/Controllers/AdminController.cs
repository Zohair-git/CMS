using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
		private readonly db_cmsContext _context;
       
        public AdminController(db_cmsContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> ProductAdd(AllTables model, List<IFormFile> imgs, IFormFile banner)
		{
			var assddssad = model.productss.ProductName;

			if (banner != null && banner.Length > 0)
			{
				var fileExt = System.IO.Path.GetExtension(banner.FileName).Substring(1);

				var random = Path.GetFileName(banner.FileName);

				var FileName = Guid.NewGuid() + random;

				string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/myImages");

				if (!Directory.Exists(imgFolder))
				{
					Directory.CreateDirectory(imgFolder);
				}
				string filepath = Path.Combine(imgFolder, FileName);
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					banner.CopyTo(stream);
				}
				var dbAddress = Path.Combine("myImages", FileName);
				model.productss.Image = dbAddress;




				// Save product data to tbl_product
				_context.TblProducts.Add(model.productss);
				_context.SaveChanges();

				// Save images to tbl_image
				foreach (var img in imgs)
				{
					var filesExt = System.IO.Path.GetExtension(img.FileName).Substring(1);

					var randoms = Path.GetFileName(img.FileName);

					var FilesName = Guid.NewGuid() + random;

					string imgsFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/myImages");

					if (!Directory.Exists(imgsFolder))
					{
						Directory.CreateDirectory(imgsFolder);
					}

					string filespath = Path.Combine(imgsFolder, FilesName);

					if (img != null && img.Length > 0)
					{
						using (var stream = new FileStream(filespath, FileMode.Create))
						{
							img.CopyTo(stream);

							var dbAddresses = Path.Combine("myImages", FilesName);
							var image = new TblImage
							{
								PId = model.productss.Id, // foreign key relationship
								ImageName = dbAddresses,
								AltTxt = "Image Alt Text", // Set your Alt text accordingly
							};

							_context.TblImages.Add(image);
							await _context.SaveChangesAsync();
						}
					}
				}
				var sadsa = "sdsad";
				return RedirectToAction("ProductView"); // Redirect to a view after successful submission
			}

			return View();
		}
		public IActionResult ProductView()
        {
            return View(_context.TblProducts.ToList());
        }
		[HttpGet]
        public IActionResult AddEvent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEvent(TblUpcomingEvent newevent , IFormFile banner)
        {

			if (banner != null && banner.Length > 0)
			{
				// GETTING IMAGE FILE EXTENSION 
				var fileExt = System.IO.Path.GetExtension(banner.FileName).Substring(1);

				// GETTING IMAGE NAME
				var random = Path.GetFileName(banner.FileName);

				// GUID ID COMBINE WITH IMAGE NAME - TO ESCAPE IMAGE NAME REDENDNCY 
				var FileName = Guid.NewGuid() + random;

				// GET PATH OF CUSTOM IMAGE FOLDER
				string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/BannerImgs");

				// CHECKING FOLDER EXIST OR NOT - IF NOT THEN CREATE F0LDER 
				if (!Directory.Exists(imgFolder))
				{
					Directory.CreateDirectory(imgFolder);
				}

				// MAKING CUSTOM AND COMBINE FOLDER PATH WITH IMAHE 
				string filepath = Path.Combine(imgFolder, FileName);

				// COPY IMAGE TO REAL PATH TO DEVELOPER PATH
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					banner.CopyTo(stream);
				}

				// READY SEND PATH TO  IMAGE TO DB  
				var dbAddress = Path.Combine("BannerImgs", FileName);

				// EQUALIZE TABLE (MODEL) PROPERTY WITH CUSTOM PATH 
				newevent.Banner = dbAddress;
				//MYIMAGES/imagetodbContext.JGP

				// SEND TO TABLE 
				_context.TblUpcomingEvents.Add(newevent);

				//SSAVE TO DB 

				_context.SaveChanges();

				return RedirectToAction("ViewEvent");
			}
			return View();
        }
        public IActionResult ViewEvent()
        {
            return View(_context.TblUpcomingEvents.ToList());
        }
        public IActionResult Bookings()
        {
            return View();
        }
		public IActionResult RegisteredUser()
		{
			return View();
		}
		public IActionResult Feedbacks()
		{
			return View();
		}
		public IActionResult AboutUs()
        {
            return View();
        }
    }
}
