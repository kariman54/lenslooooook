using lensLook.Bll;
using lensLook.Dal;
using lensLook.Dal.Models;
using lensLook.Pl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Claims = Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using lensLook.Pl.Helper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using lensLook.Dal.Context;


namespace lensLook.Pl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly LensLookDbContext context;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly UserManager<user> usermanager;
        private readonly IOrderService _orderServies;
        private readonly IServicesRepo ServiesRepo;

        public AccountController(LensLookDbContext context,RoleManager<IdentityRole> Rolemanager,UserManager<user> usermanager, IOrderService OrderServies, IServicesRepo _ServiesRepo)
        {
            this.context = context;
            rolemanager = Rolemanager;
            this.usermanager = usermanager;
            _orderServies = OrderServies;
            ServiesRepo = _ServiesRepo;
        }
        public IActionResult Users()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Alluser = usermanager.Users.Where(x => x.Id != userId).ToList();
            return View(Alluser);
        }



        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = usermanager.Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    return View(user);

                }
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteUser(user Model)
        {
            if (!string.IsNullOrEmpty(Model.Id))
            {
                var user = usermanager.Users.FirstOrDefault(x => x.Id == Model.Id);
                if (user != null)
                {
                    await usermanager.DeleteAsync(user);

                }
            }

            return RedirectToAction("Index", "Account", new { area = "Admin" });
        }






        [HttpGet]
        public IActionResult RegisterNewPatient()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> RegisterNewPatient(RegisterVM Model)
        {
            if (ModelState.IsValid)
            {
                var User = new user()
                {
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    UserName = Model.Email.Split("@")[0],
                    Email = Model.Email.Trim().ToLower(),
                    IsActive = Model.IsActive,
                    DisplayName = Model.FirstName + Model.LastName,
                    PhoneNumber = string.Concat("+2", Model.PhoneNumber),
                    DateOfBirth = Model.DateOfBirth,
                    RoleName = "Patient",
                };

                var Resulate = await usermanager.CreateAsync(User, Model.Password);

                if (Resulate.Succeeded)
                {
                    var RolenName = rolemanager.Roles.FirstOrDefault(x => x.Name == "Patient");
                    context.BasketCustomers.Add(new BasketCustomer() { UserId = User.Id });
                    context.SaveChanges();
                    await usermanager.AddToRoleAsync(User, RolenName.ToString());
                    return RedirectToAction(nameof(Users));
                }
                else
                {
                    foreach (var item in Resulate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(Model);
        }




        [HttpGet]
        public IActionResult RegisterNewDoctor()
        {
            return View();
        }




        [HttpPost]
        public async Task< IActionResult> RegisterNewDoctor(RegisterVM Model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await DocumentSetting.UploadFillesAsync(Model.Image, "Images");
                var DuplicateEmail = await usermanager.FindByEmailAsync(Model.Email);
                if (DuplicateEmail != null)
                {
                    ModelState.AddModelError(string.Empty, "Emil already exist");
                    return View();

                }

                var User = new user()
                {
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    UserName = Model.Email.Split("@")[0],
                    Email = Model.Email.Trim().ToLower(),
                    PhoneNumber = Model.PhoneNumber,
                    DisplayName = Model.FirstName + Model.LastName,
                    Experience = Model.Experience,
                    Specialization = Model.Specialization,
                    HomeAddress = Model.HomeAddress,
                    MedicalLicenseNumber = Model.MedicalLicenseNumber,
                    DateOfBirth = Model.DateOfBirth,
                    image = uniqueFileName,
                    RoleName = "Doctor",
                    NumberOfAppointments = Model.NumberOfAppointments

                };
                var Resulate = await usermanager.CreateAsync(User, Model.Password);
                if (Resulate.Succeeded)
                {
                    var RolenName = rolemanager.Roles.FirstOrDefault(x => x.Name == "Doctor");
                    context.BasketCustomers.Add(new BasketCustomer() { UserId = User.Id });
                    context.SaveChanges();
                    await usermanager.AddToRoleAsync(User, RolenName.ToString());
                    return RedirectToAction(nameof(Users));
                }
                else
                {
                    foreach (var item in Resulate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View();






        }
    }
    }
