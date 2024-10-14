//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStorageBroker storageBroker;

        public HomeController(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

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
    }
}
