//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.Foundations.Newss;
using RealNest.Web.Services.Foundations.Houses;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IStorageBroker storageBroker;
        private readonly IHouseService houseService;

        public AdminController(
            IWebHostEnvironment webHostEnvironment,
            IStorageBroker storageBroker,
            IHouseService houseService)
        {
            this.webHostEnvironment = webHostEnvironment;
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
        //----------------------//
        [HttpGet]
        public async ValueTask<IActionResult> BlogAdmin()
        {
            var blogs = await this.storageBroker.SelectAllNewssAsync();
            return View(blogs);
        }


        public IActionResult BlogAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlogAdd([FromForm] News blog, IFormFile picture)
        {
            if (picture != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "imagess");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                blog.NewsPicture = $"imagess/{fileName}";
                blog.CreatedDate= DateTime.Now;
            }

           await this.storageBroker.InsertNewsAsync(blog);
            return RedirectToAction("BlogAdmin");
        }

        public async ValueTask<IActionResult> BlogEdit(int id)
        {
            var blog = await this.storageBroker.SelectNewsByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        public async ValueTask<IActionResult> BlogEdit(int id, News news, IFormFile NewPicture)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (NewPicture != null && NewPicture.Length > 0)
                {
                    string newFileName = $"{Guid.NewGuid()}_{NewPicture.FileName}";
                    string filePath = Path.Combine("wwwroot/imagess", newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewPicture.CopyToAsync(stream);
                    }

                    news.NewsPicture = $"imagess/{newFileName}";

                }
                await this.storageBroker.UpdateNewsAsync(news);
                return RedirectToAction("BlogAdmin");
            }
            return View(news);
        }

        public async ValueTask<IActionResult> BlogDelete(int id)
        {
            var blog = await this.storageBroker.SelectNewsByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        public async ValueTask<IActionResult> DeleteConfirmed(int id)
        {
            var news = await storageBroker.SelectNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            await storageBroker.DeleteNewsAsync(news);
            return RedirectToAction("BlogAdmin");
        }
    }
}
