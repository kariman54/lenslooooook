using lensLook.Bll;
using lensLook.Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lensLook.Pl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
          var AllOrder=  _orderService.GetallOrder();
            return View(AllOrder);
        }


        public IActionResult Approval(int id)
            {
             var order =_orderService.GetOrderById(id);
            order.Status = "Success";
            _orderService.Update(order);
            return RedirectToAction(nameof(Index));
            }


        public IActionResult rejected(int id)
        {
            var order = _orderService.GetOrderById(id);
            order.Status = "rejected";
            _orderService.Update(order);
            return RedirectToAction(nameof(Index));
        }

    }
}
