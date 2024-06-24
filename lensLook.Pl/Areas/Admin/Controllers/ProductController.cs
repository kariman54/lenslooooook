using lensLook.Dal;
using lensLook.Dal.models;
using lensLook.Pl.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lensLook.Pl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepo productRepo;

        public ProductController( IProductRepo ProductRepo)
        {
            productRepo = ProductRepo;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var AllProduct= productRepo.GetAllProduct();
            return View(AllProduct);
        }

        public IActionResult  Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product Model , IFormFile ImageProduct)
        {
            if (ModelState.IsValid)
            {
                Model.PictureUrl =await DocumentSetting.UploadFillesAsync(ImageProduct, "ProductImage");
                productRepo.Create(Model);
                return RedirectToAction(nameof(Index));

            }
            return View(Model);

        }





        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var Product=productRepo.GetProductById(Id);
            return View(Product);
        }




        [HttpPost]
        public IActionResult Delete(Product Model)
        {

                productRepo.Delete(Model.Id);

            return RedirectToAction(nameof(Index));

        }


    }
}
