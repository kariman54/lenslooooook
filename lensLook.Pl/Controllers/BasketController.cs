using lensLook.Dal;
using lensLook.Dal.models;
using lensLook.Dal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lensLook.Pl.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketRepo _basketRepo;
        private readonly UserManager<user> userManager;
        private readonly IProductRepo productRepo;

        public BasketController(IBasketRepo BasketRepo, UserManager<user> userManager, IProductRepo productRepo)
        {
            _basketRepo = BasketRepo;
            this.userManager = userManager;
            this.productRepo = productRepo;
        }


        [Authorize]
        public async Task<IActionResult> AddToCart(int Product)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var OldBasket = _basketRepo.GetCustomerBasket(userId);

            var x = OldBasket.BasketItems.Where(x => x.Productid == Product).FirstOrDefault();
            if (x != null)
            {
                x.Quantity++;
            }
            else
            {
                var product = productRepo.GetProductById(Product);
                OldBasket.BasketItems.Add(new BasketItems
                {
                    Productid = Product,
                    Quantity = 1,
                    Photo = product.PictureUrl,
                    price = product.Price,
                    Name = product.Name,
                });

            }

            var StateUpdateOrDelete = _basketRepo.UpdateBasket(OldBasket);

            return RedirectToAction("store", "Home");


        }


        [Authorize]
        public IActionResult Cart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var OldBasket = _basketRepo.GetCustomerBasketWithProduct(userId);
            return View(OldBasket);
        }

        [Authorize]
        public IActionResult IncrementProductFromBasket(int Id  /* ProductId*/  )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var OldBasket = _basketRepo.GetCustomerBasket(userId);

            var x = OldBasket.BasketItems.Where(x => x.Productid == Id).FirstOrDefault();
            if (x != null)
            {
                x.Quantity++;
            }


            var StateUpdateOrDelete = _basketRepo.UpdateBasket(OldBasket);

            return RedirectToAction("Cart", "Basket");


        }       

        public IActionResult LessProductFromBasket(int Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var OldBasket = _basketRepo.GetCustomerBasket(userId);

            var x = OldBasket.BasketItems.Where(x => x.Productid == Id).FirstOrDefault();
            if (x != null)
            {
                if(x.Quantity>2)
                x.Quantity--;
                else
                {
                    OldBasket.BasketItems.Remove(x);
                }
            }


            var StateUpdateOrDelete = _basketRepo.UpdateBasket(OldBasket);

            return RedirectToAction("Cart", "Basket");
        }

        public IActionResult RemoveProductFromBasket(int Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var OldBasket = _basketRepo.GetCustomerBasket(userId);

            var x = OldBasket.BasketItems.Where(x => x.Productid == Id).FirstOrDefault();

            OldBasket.BasketItems.Remove(x);

            var StateUpdateOrDelete = _basketRepo.UpdateBasket(OldBasket);

            return RedirectToAction("Cart", "Basket");
        }

















    }
}
