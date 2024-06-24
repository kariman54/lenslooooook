using lensLook.Bll;
using lensLook.Dal;
using lensLook.Dal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lensLook.Pl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<user> _usermanager;
        private readonly IOrderService _orderServies;
        private readonly IRequestServices ServiesRepo;
        public HomeController(UserManager<user> usermanager, IOrderService OrderServies, IRequestServices _ServiesRepo)
        {
            _orderServies= OrderServies;
            ServiesRepo= _ServiesRepo;
            _usermanager = usermanager;
        }
        public IActionResult Index()
        {
            ViewBag.totalUser=_usermanager.Users.Count();
            ViewBag.TotalOrder= _orderServies.TotalOrders();
            ViewBag.TotalRequests= ServiesRepo.TotalServices();
            ViewBag.TotalRequestPending= ServiesRepo.TotalServicesPending();
            ViewBag.TotalRequestSuccess = ServiesRepo.TotalServicesSuccess();
            return View();
        }
    }
}
