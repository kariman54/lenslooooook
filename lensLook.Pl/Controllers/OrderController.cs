using lensLook.Bll.Services;
using lensLook.Dal;
using lensLook.Dal.Models;
using lensLook.Pl.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lensLook.Pl.Controllers
{
	public class OrderController : Controller
	{
		private readonly IBasketRepo basketRepo;
		private readonly IProductRepo productRepo;
        private readonly IOrderService _orderServices;

        public OrderController(IBasketRepo _BasketRepo,IProductRepo productRepo , IOrderService orderServices)
        {
			basketRepo = _BasketRepo;
			this.productRepo = productRepo;
            _orderServices = orderServices;
        }


		[HttpGet]
		public IActionResult CreateOrder(int BasketCustomer)
		{
            var Basket = basketRepo.GetCustomerBasketWithProductById(BasketCustomer);
            if(Basket==null)
            {
                return RedirectToAction("Index", "Home");
            }
            var UserEmail = User.FindFirstValue(claimType: ClaimTypes.Email);

            var orderItems = new List<OrderItem>();

            foreach (var item in Basket.BasketItems)
            {
                var Product = productRepo.GetProductById(item.Productid);

                orderItems.Add(new OrderItem(new ProductItemOrder(item.Id, item.Name, item.Product.PictureUrl), item.price, item.Quantity));

            }

            var Orders = new Order(UserEmail, "", Basket.TotalPrice, orderItems);
            return View(Orders);

        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order model)
        {

            var UserId= User.FindFirstValue(claimType:ClaimTypes.NameIdentifier);
            var Basket=basketRepo.GetCustomerBasket(UserId);
            var BasketWithOrder = basketRepo.GetCustomerBasketWithProductById(Basket.Id);

            var orderItems = new List<OrderItem>();

            foreach (var item in Basket.BasketItems)
            {
                var Product = productRepo.GetProductById(item.Productid);

                orderItems.Add(new OrderItem(new ProductItemOrder(item.Id, item.Name, item.Product.PictureUrl), item.price, item.Quantity));

            }

            model.UserIdCreateOrder = UserId;
            model.Items = orderItems;

            await _orderServices.CreateOrderAsync(model);
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult AllOrderForUser()
        {
            var UserId = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            var orders=_orderServices.GetOrdersForUser(UserId);  


            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int orderId)
        {

            var orders = _orderServices.GetOrderById(orderId);



                return View(orders);
        }


        [HttpGet]
        public IActionResult RemoveOrder(int orderId)
        {
            _orderServices.RemoveOrder(orderId);
            return RedirectToAction("Cart","Basket");
        }

    }
}
