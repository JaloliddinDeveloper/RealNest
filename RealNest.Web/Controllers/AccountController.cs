//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Models.ViewModels;
using RealNest.Web.Services.Foundations.Users;

namespace RealNest.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService) =>
            this.userService = userService;

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                };
                var result = await userService.AddUserAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}

