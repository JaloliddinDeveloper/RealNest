//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Models.ViewModels;

namespace RealNest.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async ValueTask<IActionResult> Create(UserViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new User
        //        {
        //            Username = viewModel.Username,
        //            Email = viewModel.Email
        //        };
        //       await this.
        //        return RedirectToAction("Index");
        //    }
        //    return View(viewModel);
        //}
    }
}
