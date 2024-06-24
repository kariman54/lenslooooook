using lensLook.Dal;
using lensLook.Dal.models;
using lensLook.Pl.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lensLook.Pl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepo _ProductRepo;

        public HomeController(ILogger<HomeController> logger, IProductRepo ProductRepo)
        {
            _logger = logger;
            _ProductRepo = ProductRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VisionCheck()
        {
            return View();
        }

        public IActionResult EyeCare()
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






        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult aboutUS()
        {
            return View();
        }



        public IActionResult OurDoctors()
        {
            return View();
        }
        public IActionResult Appointment()
        {
            return View();
        }   
        
        public IActionResult Services()
        {
            return View();
        }


        public IActionResult Reviews()
        {
            return View();
        }

        public IActionResult store()
        {
            ViewBag.AllProducts = _ProductRepo.GetAllProduct() as IEnumerable<Product>;

            return View();
        }



    }
}
