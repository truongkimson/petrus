using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private string userId;
        public AdoptionListingController(petrusDb context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            //hard coded for now, have to retrieve user data from login details to determine id.
            userId = "10";
            var listings = dbContext.AdoptionListings.Where(x=>x.UserID==userId).OrderBy(o=>o.AdoptionListingID.Length).ThenBy(a=>a.AdoptionListingID).ToList();

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

            if ((model.PetImage != null)&&checkFileSignature(model.PetImage,".jpg"))
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

            if ((model.PetVideo != null)&& checkFileSignature(model.PetVideo, ".mp4"))
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


        private static readonly Dictionary<string, List<byte[]>> _fileSignature =new Dictionary<string, List<byte[]>>
        {
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".mp4", new List<byte[]>
                {
                    new byte[] { 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D },
                    new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56 },
                    new byte[] { 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 },
                }
            }
        };
        private bool checkFileSignature(IFormFile someFile, string ext)
        {
            Stream uploadedFileData = someFile.OpenReadStream();
            bool output=false;
            if (ext.Equals(".jpg"))
            {
                using (var reader = new BinaryReader(uploadedFileData))
                {
                    var signatures = _fileSignature[ext];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    output = signatures.Any(signature =>
                         headerBytes.Take(signature.Length).SequenceEqual(signature));
                }
            }
            if (ext.Equals(".mp4"))
            {
                using (var reader = new BinaryReader(uploadedFileData))
                {
                    var signatures = _fileSignature[ext];
                    var headerBytes = reader.ReadBytes(4+signatures.Max(m => m.Length));
                    output = signatures.Any(signature =>
                         headerBytes.Skip(4).Take(signature.Length).SequenceEqual(signature));
                }
            }
            
            uploadedFileData.Close();
            return output;
        }

        [HttpPost]
        public IActionResult ViewRequests([FromForm] string selected)
        {
            if (selected != null)
            {
                AdoptionListing listing = dbContext.AdoptionListings.FirstOrDefault(x => x.AdoptionListingID == selected);
                if (listing.AdoptionRequests.Count > 0)
                {
                    if (listing != null)
                    {
                        List<AdoptionRequest> requests = listing.AdoptionRequests.ToList();
                        ViewData["requests"] = requests;
                        ViewData["listing"] = listing;
                    }
                }
                else
                    return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
            return View();
        }
        public IActionResult Update(string id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdoptionListingViewModel model)
        {
            AdoptionListing myOldListing = dbContext.AdoptionListings.FirstOrDefault(x => x.AdoptionListingID == Convert.ToString(model.id));
            if (ModelState.IsValid)
            {
                deleteFile(myOldListing.Image);
                deleteFile(myOldListing.Video);
                string[] uniqueFileNames = UploadedFile(model);
                myOldListing.Species = model.Species;
                myOldListing.Name = model.Name;
                myOldListing.Breed1 = model.Breed1;
                myOldListing.Breed2 = model.Breed2;
                myOldListing.Color1 = model.Color1;
                myOldListing.Color2 = model.Color2;
                myOldListing.Gender = model.Gender;
                myOldListing.Age = model.Age;
                myOldListing.QuantityRepresented = model.Quantity;
                myOldListing.Fee = model.Fee;
                myOldListing.Vaccinated = model.Vaccinated;
                myOldListing.Dewormed = model.Dewormed;
                myOldListing.Sterilized = model.Sterilized;
                myOldListing.Health = model.Health;
                myOldListing.FurLength = model.FurLength;
                myOldListing.Image = uniqueFileNames[0];
                myOldListing.Video = uniqueFileNames[1];
                myOldListing.ListingDate = DateTime.Now;
                dbContext.AdoptionListings.Update(myOldListing);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
            
        }
        protected void deleteFile(String name)
        {
            if (name.Contains("png") || name.Contains("jpg"))
            {
                string fileFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(fileFolder, name);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            if (name.Contains("mp4"))
            {
                string fileFolder = Path.Combine(webHostEnvironment.WebRootPath, "videos");
                string filePath = Path.Combine(fileFolder, name);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists) 
                {
                    file.Delete();
                }
            }
        }
    }
            
}
