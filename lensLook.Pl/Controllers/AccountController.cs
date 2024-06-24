using lensLook.Dal.Context;
using lensLook.Dal.Models;
using lensLook.Pl.Helper;
using lensLook.Pl.Models;
using lensLook.PL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace lensLook.Pl.Controllers
{
    public class AccountController : Controller
    {
        private readonly LensLookDbContext context;
        private readonly UserManager<user> _usermanager; // To sign IN User;
        private readonly SignInManager<user> _signManager  /*to make User Create*/;
        private readonly IEmailSettings _Mailmanager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(LensLookDbContext Context,UserManager<user> usermanager, SignInManager<user> SignManager, IEmailSettings mailmanager,RoleManager<IdentityRole> roleManager)
        {
            context = Context;
            _usermanager = usermanager;
            _signManager = SignManager;
            _Mailmanager = mailmanager;
            this.roleManager = roleManager;
        }








        [HttpPost]
        public async Task<IActionResult> RegisterOrLogin(ModelLoginAndRegister Model)
        {
            if (ModelState.IsValid)
            {
                if (Model.ModelLogin != null)
                {
                    await Login(Model.ModelLogin);
                }
                else if (Model.ForgetPassword != null)
                {
                    await SendEmail(Model.ForgetPassword);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(ModelLoginAndRegister Model)
        {
            if (ModelState.IsValid)
            {
                var User = new user()
                {
                    FirstName = Model.ModelRegister.FirstName,
                    LastName = Model.ModelRegister.LastName,
                    UserName = Model.ModelRegister.Email.Split("@")[0],
                    Email = Model.ModelRegister.Email.Trim().ToLower(),
                    IsActive = Model.ModelRegister.IsActive,
                    DisplayName = Model.ModelRegister.FirstName + Model.ModelRegister.LastName,
                    PhoneNumber = string.Concat("+2", Model.ModelRegister.PhoneNumber),
                    DateOfBirth = Model.ModelRegister.DateOfBirth,
                    RoleName = "Patient",
                };

                var Resulate = await _usermanager.CreateAsync(User, Model.ModelRegister.Password);

                if (Resulate.Succeeded)
                {
                    var RolenName = roleManager.Roles.FirstOrDefault(x => x.Name == "Patient");
                    context.BasketCustomers.Add(new BasketCustomer() {UserId= User.Id });
                    await _usermanager.AddToRoleAsync(User, RolenName.ToString());
                    return RedirectToAction(nameof(Login));
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
        public IActionResult Login()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        #region Login



        [HttpPost]
        public async Task<IActionResult> Login(LoginVM UserLogin)
        {
            if (ModelState.IsValid)
            {
                var User = await _usermanager.FindByEmailAsync(UserLogin.Email.Trim().ToLower());
                if (User is not null)
                {
                    var flag = await _usermanager.CheckPasswordAsync(User, UserLogin.Password);
                    if (flag)
                    {
                        var Resulate = await _signManager.PasswordSignInAsync(User, UserLogin.Password, UserLogin.RememberMe, false);
                        if (Resulate.Succeeded)
                        {
                            if(User.RoleName=="Admin")
                            {
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            return RedirectToAction("Index", "home");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Password is not Correct Yet");

                }
                ModelState.AddModelError(string.Empty, "Email is not Registeration Yet");

            }
            return View();
        }
        #endregion


        #region SignOut

        public new async Task<IActionResult> SignOut()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }



        #endregion


        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]





        public async Task<IActionResult> SendEmail(ForgetPasswordVM Model)
        {
            if (ModelState.IsValid)
            {
                var User = await _usermanager.FindByEmailAsync(Model.Email);

                if (User is not null)
                {
                    var TokenResetPassword = await _usermanager.GeneratePasswordResetTokenAsync(User);
                    var PasswordLink = Url.Action("ResetPassword", "account", new { email = Model.Email, token = TokenResetPassword }, Request.Scheme);

                    Email email = new Email()
                    {
                        Subject = "Reset PassWord ",
                        To = User.Email,
                        Body = PasswordLink,
                    };
                    //_mailmanager.SendMail(email);
                    _Mailmanager.SendEmail(email);
                    return RedirectToAction(nameof(checkyourinbox));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Existed");
                    return View("ForgetPassword", Model);
                }

            }
            return View("ForgetPassword", Model);
        }









        //public async Task<IActionResult> SendSMS(ForgetPasswordVM Model)
        //{
        //	if (ModelState.IsValid)
        //	{




        //		var User = await _usermanager.FindByEmailAsync(Model.Email);

        //		if (User is not null)
        //		{
        //			var TokenResetPassword = await _usermanager.GeneratePasswordResetTokenAsync(User);
        //			var PasswordLink = Url.Action("ResetPassword", "account", new { email = Model.Email, token = TokenResetPassword }, Request.Scheme);

        //			var sms = new SmsMessage()
        //			{
        //				Body = PasswordLink,
        //				NumberPhone = User.PhoneNumber
        //			};
        //			_smsServices.send(sms);

        //			return Ok("Check Phone Number");
        //		}
        //		else
        //		{
        //			ModelState.AddModelError(string.Empty, "Email is not Existed");
        //			return View("ForgetPassword", Model);
        //		}

        //	}
        //	return View("ForgetPassword", Model);
        //}














        public IActionResult checkyourinbox()
        {
            return View();
        }

        #endregion

        #region Reset Password 
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassViewModel model)
        {
            var email = TempData["email"] as string;
            var token = TempData["token"] as string;
            var user = await _usermanager.FindByEmailAsync(email);

            if (ModelState.IsValid)
            {
                var result = await _usermanager.ResetPasswordAsync(user, token, model.newpassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);


        }

        #endregion



        #region Auth With Google 

        //public IActionResult LoginWithAuth()
        //{
        //	var prop = new AuthenticationProperties
        //	{
        //		RedirectUri = Url.Action("GoogleResponse")
        //	};
        //	return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        //}




        //public async Task<IActionResult> GoogleResponse(string issuer)
        //{
        //	var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        //	var cliams = result.Principal.Identities.FirstOrDefault().Claims.Select(x =>
        //	new
        //	{
        //		x.Issuer,
        //		x.OriginalIssuer,
        //		x.Type,
        //		x.Value
        //	});
        //	return RedirectToAction("index", "home");
        //}
        #endregion

        public IActionResult DoctorRegestratoin()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoctorRegestratoin(ModelLoginAndRegister Model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName =await  DocumentSetting.UploadFillesAsync(Model.ModelRegister.Image, "Images");
                var DuplicateEmail = await _usermanager.FindByEmailAsync(Model.ModelRegister.Email);
                if(DuplicateEmail!=null)
                {
                    ModelState.AddModelError(string.Empty, "Emil already exist");
                    return View();

                }


                var User = new user()
                {
                    FirstName = Model.ModelRegister.FirstName,
                    LastName = Model.ModelRegister.LastName,
                    UserName = Model.ModelRegister.Email.Split("@")[0],
                    Email = Model.ModelRegister.Email.Trim().ToLower(),
                    PhoneNumber = Model.ModelRegister.PhoneNumber,
                    DisplayName = Model.ModelRegister.FirstName + Model.ModelRegister.LastName,
                    Experience = Model.ModelRegister.Experience,
                    Specialization = Model.ModelRegister.Specialization,
                    HomeAddress = Model.ModelRegister.HomeAddress,
                    MedicalLicenseNumber = Model.ModelRegister.MedicalLicenseNumber,
                    DateOfBirth=Model.ModelRegister.DateOfBirth,
                    image = uniqueFileName,
                    RoleName = "Doctor",
                    NumberOfAppointments = Model.ModelRegister.NumberOfAppointments

                };
                var Resulate = await _usermanager.CreateAsync(User, Model.ModelRegister.Password);
                if (Resulate.Succeeded)
                {
                    var RolenName= roleManager.Roles.FirstOrDefault(x=>x.Name== "Doctor");
                    context.BasketCustomers.Add(new BasketCustomer() { UserId = User.Id });
                    context.SaveChanges();
                    await  _usermanager.AddToRoleAsync(User, RolenName.ToString());
                    return RedirectToAction(nameof(Login));
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

    }
}
