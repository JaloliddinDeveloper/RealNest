//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Models.ViewModels.Register;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountController(
            IStorageBroker storageBroker,
            IPasswordHasher<User> passwordHasher)
        {
            this.storageBroker = storageBroker;
            this.passwordHasher = passwordHasher;
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
                user.Password = this.passwordHasher.HashPassword(user, model.Password);

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
                    var result = this.passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

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
    }
}
