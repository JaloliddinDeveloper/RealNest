//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
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
                return NotFound(); // Uy topilmasa yoki yaroqsiz bo'lsa, xato qaytariladi
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
            if (!string.IsNullOrWhiteSpace(searchInput))
            {
                var houses = await storageBroker.SearchHousesAsync(searchInput);
                var validHouses = houses.Where(h => h.IsValable);
                return View(validHouses);
            }

            var allHouses = await storageBroker.SelectHousesWithPicturesAsync();
            var validAllHouses = allHouses.Where(h => h.IsValable);
            return View(validAllHouses);
        }

        public async Task<IActionResult> New()
        {
            var houses = await storageBroker.SelectNewHousesWithPicturesAsync();
            var validHouses = houses.Where(h => h.IsValable);
            return View(validHouses);
        }
    }
}
