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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email
                };
                user.Password = this.passwordHasher.HashPassword(user, model.Password);

                await this.storageBroker.InsertUserAsync(user);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Email bo'yicha foydalanuvchini qidirish
                var user = await this.storageBroker.SelectUserByEmailAsync(model.Email);

                if (user != null)
                {
                    // Parolni tekshirish
                    var result = this.passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        // Muvaffaqiyatli login bo'lganda, foydalanuvchining ID sini sessiyaga yozish
                        HttpContext.Session.SetString("UserId", user.UserId.ToString());

                        // Foydalanuvchini House yaratish sahifasiga yo'naltirish
                        return RedirectToAction("Create", "House");
                    }
                }

                // Agar foydalanuvchi yoki parol xato bo'lsa, xato xabari qo'shish
                ModelState.AddModelError("", "Login yoki parol xato.");
            }

            // Agar model to'g'ri bo'lmasa, login sahifasini qaytarish
            return View(model);
        }
    }
}
