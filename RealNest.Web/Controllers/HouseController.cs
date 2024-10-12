//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.ViewModels;
using RealNest.Web.Services.Foundations.Houses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class HouseController : Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly IHouseService houseService;

        public HouseController(
            IStorageBroker storageBroker, 
            IHouseService houseService)
        {
            this.storageBroker = storageBroker;
            this.houseService = houseService;
        }

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
                    return RedirectToAction("HouseList", "House");
                }

                ModelState.AddModelError("", "Foydalanuvchi ma'lumotlari topilmadi.");
            }

            return View(model);
        }

        [HttpGet]
        public async ValueTask<IActionResult> HouseList()
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                IQueryable<House> userHouses = await storageBroker.SelectHousesByUserIdAsync(userId);

                return View(userHouses.ToList());
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public async ValueTask<IActionResult> EditHouse(Guid id)
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                House houseToEdit = await storageBroker.SelectHouseByIdAsync(id);

                if (houseToEdit != null && houseToEdit.UserId == userId)
                {
                    return View(houseToEdit);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "House not found or does not belong to you.");
                    return RedirectToAction("HouseList");
                }
            }
            return RedirectToAction("HouseList");
        }

        [HttpPost]
        public async ValueTask<IActionResult> EditHouse(House house)
        {
            if (ModelState.IsValid)
            {
                var existingHouse = await storageBroker.SelectHouseByIdAsync(house.Id);

                if (existingHouse != null)
                {
                    if (await storageBroker.UserExistsAsync(house.UserId))
                    {
                        existingHouse.Title = house.Title; 

                        await storageBroker.UpdateHouseAsync(existingHouse);
                        return RedirectToAction("HouseList");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid UserId.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "House not found.");
                }
            }
            return View(house); 
        }
    }
}
