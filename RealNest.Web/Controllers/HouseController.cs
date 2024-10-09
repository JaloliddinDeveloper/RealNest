//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class HouseController : Controller
    {
        private readonly IStorageBroker storageBroker;

        public HouseController(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        [HttpGet]
        public IActionResult Create() =>
             View();

        [HttpPost]
        public async Task<IActionResult> Create(HouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userIdString = HttpContext.Session.GetString("UserId");

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    var house = new House
                    {
                        Title = model.Title,
                        UserId = userId
                    };

                    await this.storageBroker.InsertHouseAsync(house);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Foydalanuvchi ma'lumotlari topilmadi.");
            }

            return View(model);
        }
    }
}
