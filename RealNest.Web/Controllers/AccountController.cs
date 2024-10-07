//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Models.ViewModels;
using RealNest.Web.Services.Foundations.Users;

namespace RealNest.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountController(
            IUserService userService,
            IPasswordHasher<User> passwordHasher)
        {
            this.userService = userService;
            this.passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult SignUp() =>
            View();

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email
                };

                user.PasswordHash = this.passwordHasher.HashPassword(user, model.Password);

                await this.userService.AddUserAsync(user);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
