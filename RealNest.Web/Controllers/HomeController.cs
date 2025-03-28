//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStorageBroker storageBroker;

        public HomeController(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> AllHouses()
        {
            var allHouses = await storageBroker.SelectHousesWithPicturesAsync();
            var validHouses = allHouses.Where(h => h.IsValable);
            return View(validHouses);
        }

        [HttpGet]
        public IActionResult StreamVideo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Invalid file name.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Videos", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Video not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var mimeType = "video/mp4";

            return File(fileStream, mimeType, enableRangeProcessing: true);
        }


        [HttpGet]
        public async Task<IActionResult> AllForBuyHouses()
        {
            var allHouses = await storageBroker.SelectHouseForBuyWithPicturesAsync();
            var validHouses = allHouses.Where(h => h.IsValable);
            return View(validHouses);
        }

        [HttpGet]
        public async Task<IActionResult> AllForRentHouses()
        {
            var allHouses = await storageBroker.SelectHouseForRentWithPicturesAsync();
            var validHouses = allHouses.Where(h => h.IsValable);
            return View(validHouses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var house = await storageBroker.SelectHouseWithPictures(id);

            if (house == null || !house.IsValable)
            {
                return NotFound();
            }

            return View(house);
        }

        public async Task<IActionResult> ShowVideo(int id)
        {
            var house = await storageBroker.SelectHouseWithPictures(id);

            if (house == null || !house.IsValable)
            {
                return NotFound();
            }

            return View(house);
        }

        public async Task<IActionResult> HouseImages(int id)
        {
            var house = await storageBroker.SelectHouseWithPictures(id);

            if (house == null || !house.IsValable)
            {
                return NotFound();
            }

            return View(house);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchInput)
        {
            try
            {
                List<House> validHouses;

                if (!string.IsNullOrWhiteSpace(searchInput))
                {
                    var houses = await storageBroker.SearchHousesAsync(searchInput);
                    validHouses = houses.Where(h => h.IsValable).ToList();
                }
                else
                {
                    var allHouses = await storageBroker.SelectHousesWithPicturesAsync();
                    validHouses = allHouses.Where(h => h.IsValable).ToList();
                }

                if (!validHouses.Any())
                {
                    ViewBag.Message = "Uy topilmadi";
                }

                return View(validHouses);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Qidiruv jarayonida xatolik yuz berdi. Iltimos, keyinroq urinib ko'ring.";
                return View(new List<House>());
            }
        }

        public async Task<IActionResult> New()
        {
            var houses = await storageBroker.SelectNewHousesWithPicturesAsync();
            var validHouses = houses.Where(h => h.IsValable);
            return View(validHouses);
        }

        [HttpGet]
        public async ValueTask<IActionResult> Newss()
        {
            var newss = await this.storageBroker.SelectAllNewssAsyncOrderBy();
            return View(newss);
        }

        public async ValueTask<IActionResult> NewssDet(int id)
        {
            var blog = await this.storageBroker.SelectNewsByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpGet]
        public async ValueTask<IActionResult> NewssSotish()
        {
            var newsotish = await this.storageBroker.GetExpiredHousesSotishWithPicturesAsync();
            var validHouses = newsotish.Where(h => h.IsValable);
            return View(validHouses);
        }

        [HttpGet]
        public async ValueTask<IActionResult> NewssIjaragaBerish()
        {
            var newsotish = await this.storageBroker.GetExpiredHousesIjaragaBerishWithPicturesAsync();
            var validHouses = newsotish.Where(h => h.IsValable);
            return View(validHouses);
        }
    }
}
