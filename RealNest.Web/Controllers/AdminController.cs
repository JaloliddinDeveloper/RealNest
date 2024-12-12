using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Services.Foundations.Houses;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class AdminController:Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly IHouseService houseService;

        public AdminController(
            IStorageBroker storageBroker,
            IHouseService houseService)
        {
            this.storageBroker = storageBroker;
            this.houseService = houseService;
        }

       
        [HttpGet]
        public async ValueTask<IActionResult> MainAdmin()
        {
            IQueryable<House> allHouses = await storageBroker.SelectAllHousesAsync();
            return View(allHouses.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsValable(int id)
        {
            var house = await storageBroker.SelectHouseByIdAsync(id);

            if (house != null)
            {
                house.IsValable = true;
                await storageBroker.UpdateHouseAsync(house);
                TempData["SuccessMessage"] = "The house has been marked as Valable!";
            }
            else
            {
                TempData["ErrorMessage"] = "House not found!";
            }

            return RedirectToAction("MainAdmin");
        }

        public async Task<IActionResult> AdminDetails(int id)
        {
            var house = await this.storageBroker.SelectHouseWithPictures(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        public async Task<IActionResult> AdminHouseImages(int id)
        {
            var house = await storageBroker.SelectHouseWithPictures(id);

            return View(house);
        }
    }
}
