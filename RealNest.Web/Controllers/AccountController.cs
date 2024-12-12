//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Admins;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Models.ViewModels.Admins;
using RealNest.Web.Models.ViewModels.Register;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly IPasswordHasher<User> passwordHasherUser;
        private readonly IPasswordHasher<Admin> passwordHasherAdmin;

        public AccountController(
            IStorageBroker storageBroker,
            IPasswordHasher<User> passwordHasherUser,
            IPasswordHasher<Admin> passwordHasherAdmin)
        {
            this.storageBroker = storageBroker;
            this.passwordHasherUser = passwordHasherUser;
            this.passwordHasherAdmin = passwordHasherAdmin;
        }

        [HttpGet]
        public IActionResult Register()=>
            View();
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                };
                user.Password = this.passwordHasherUser.HashPassword(user, model.Password);

                await this.storageBroker.InsertUserAsync(user);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()=>
            View();
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.storageBroker.SelectUserByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = this.passwordHasherUser.VerifyHashedPassword(user, user.Password, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("UserId", user.Id.ToString());

                        return RedirectToAction("HouseList", "House");
                    }
                }
                ModelState.AddModelError("", "Login yoki parol xato.");
            }
            return View(model);
        }

        //AdminRegister
        [HttpGet]
        public IActionResult AdminRegister() =>
            View();

        [HttpPost]
        public async Task<IActionResult> AdminRegister(AdminViewModelRegister model)
        {
            if (ModelState.IsValid)
            {
                var admin = new Admin
                {
                    AdminName = model.AdminName,
                };
                admin.Password = this.passwordHasherAdmin.HashPassword(admin, model.Password);

                await this.storageBroker.InsertAdminAsync(admin);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        //AdminLogin
        [HttpGet]
        public IActionResult LoginAdmin() =>
            View();

        [HttpPost]
        public async Task<IActionResult> LoginAdmin(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await this.storageBroker.SelectAdminByEmailAsync(model.AdminName);

                if (admin != null)
                {
                    var result = this.passwordHasherAdmin.VerifyHashedPassword(admin, admin.Password, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        return RedirectToAction("MainAdmin", "Admin");
                    }
                }
                ModelState.AddModelError("", "Login yoki parol xato.");
            }
            return View(model);
        }

      
    }
}
