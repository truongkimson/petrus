using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using petrus.Data;
using petrus.Models;
using petrus.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Controllers
{
    public class AdoptionListingController : Controller
    {
        private readonly petrusDb dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdoptionListingController(petrusDb context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var listings = dbContext.AdoptionListings.ToList();
            return View(listings);
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(AdoptionListingViewModel model)
        {
            if (ModelState.IsValid)
            {
                string[] uniqueFileNames = UploadedFile(model);
                AdoptionListing myListing = new AdoptionListing
                {
                    Species = model.Species,
                    Name = model.Name,
                    Breed1 = model.Breed1,
                    Breed2 = model.Breed2,
                    Color1 = model.Color1,
                    Color2 = model.Color2,
                    Gender = model.Gender,
                    Age = model.Age,
                    QuantityRepresented = model.Quantity,
                    Fee = model.Fee,
                    Vaccinated = model.Vaccinated,
                    Dewormed = model.Dewormed,
                    Sterilized = model.Sterilized,
                    Health = model.Health,
                    FurLength = model.FurLength,
                    Image = uniqueFileNames[0],
                    Video = uniqueFileNames[1],
                    ListingDate = DateTime.Now,
                    ApplicationStatus = ApplicationStatus.Open
                };
                dbContext.AdoptionListings.Add(myListing);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string[] UploadedFile(AdoptionListingViewModel model)
        {
            string uniqueFileName = null;

            if (model.PetImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PetImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PetImage.CopyTo(fileStream);
                }
            }
            string uniqueFileName2 = null;

            if (model.PetVideo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "videos");
                uniqueFileName2 = Guid.NewGuid().ToString() + "_" + model.PetVideo.FileName;
                string filePath2 = Path.Combine(uploadsFolder, uniqueFileName2);
                using (var fileStream = new FileStream(filePath2, FileMode.Create))
                {
                    model.PetVideo.CopyTo(fileStream);
                }
            }
            string[] output = new string[] { uniqueFileName, uniqueFileName2 };
            return output;
        }
        public IActionResult Remove(string id)
        {
            if (id != null)
            {
                AdoptionListing listing = dbContext.AdoptionListings.FirstOrDefault(x => x.AdoptionListingID == id);
                if (listing != null)
                {
                    dbContext.AdoptionListings.Remove(listing);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index"); ;
        }
        public IActionResult ViewMore(string id)
        {
            if (id != null)
            {
                AdoptionListing listing = dbContext.AdoptionListings.FirstOrDefault(x => x.AdoptionListingID == id);
                if (listing != null)
                {
                    ViewData["listing"] = listing;
                }
            }
            else
                return RedirectToAction("Index");
            return View();
        }
    }
}

