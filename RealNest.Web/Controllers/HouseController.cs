//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.Foundations.Pictures;
using RealNest.Web.Models.ViewModels;
using RealNest.Web.Services.Foundations.Houses;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> Create(AddHouseViewModel model, List<IFormFile> HouseImages)
        {
            if (ModelState.IsValid)
            {
                string userIdString = HttpContext.Session.GetString("UserId");

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    if (model == null || string.IsNullOrEmpty(model.Title))
                    {
                        throw new ArgumentNullException("House object or Title cannot be null.");
                    }

                    var house = new House
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Price = model.Price,
                        Address = model.Address,
                        Location = model.Location,
                        SquareFootage = model.SquareFootage,
                        ListingType=model.ListingType,
                        ContactInformation=model.ContactInformation,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        UserId = userId
                    };

                    await this.storageBroker.InsertHouseAsync(house);

                    if (HouseImages != null && HouseImages.Any())
                    {
                        foreach (var image in HouseImages)
                        {
                            if (image.Length > 0)
                            {
                                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);

                                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

                                if (!Directory.Exists(uploadsFolder))
                                {
                                    Directory.CreateDirectory(uploadsFolder);
                                }

                                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await image.CopyToAsync(fileStream);
                                }

                                var picture = new Picture
                                {
                                    ImageUrl = "/imagens/" + uniqueFileName,
                                    HouseId = house.Id
                                };

                                await this.storageBroker.InsertPictureAsync(picture);
                            }
                        }
                    }
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
        public async Task<IActionResult> HouseImages(int id)
        {
            var house = await storageBroker.SelectHouseWithPictures(id);

            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        [HttpGet]
        public async ValueTask<IActionResult> EditHouse(int id)
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

        [HttpGet]
        public async Task<IActionResult> ConfirmRemoveHouse(int id)
        {
            House houseToRemove = await storageBroker.SelectHouseByIdAsync(id);

            if (houseToRemove != null)
            {
                return View(houseToRemove);
            }
            ModelState.AddModelError(string.Empty, "House not found.");
            return RedirectToAction("HouseList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveHouse(int id)
        {
            var result = await this.houseService.RemoveHouseAsync(id);

            if (result != null)
            {
                return RedirectToAction("HouseList");
            }

            ModelState.AddModelError(string.Empty, "Unable to delete the house. Please try again.");
            return RedirectToAction("HouseList");
        }
    }
}
