//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStorageBroker storageBroker;

        public HomeController(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Buy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Rent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllHouses()
        {
            var allHouses = await storageBroker.SelectHousesWithPicturesAsync();
            return View(allHouses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var house = await this.storageBroker.SelectHouseWithPictures(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        public async Task<IActionResult> HouseImages(int id)
        {
            var house = await storageBroker.SelectHouseWithPictures(id);

            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }
    }
}
