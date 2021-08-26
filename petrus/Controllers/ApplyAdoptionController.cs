using Microsoft.AspNetCore.Mvc;
using petrus.Data;
using petrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petrus.BindingModel;

namespace petrus.Controllers
{
    public class ApplyAdoptionController : Controller
    {
        private readonly petrusDb dbContext;

        public ApplyAdoptionController(petrusDb context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            var listings = dbContext.AdoptionListings.ToList();
            return View(listings);
        }

        public IActionResult Details(string id)
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

        public IActionResult Application(String id)
        {
            User user = dbContext.Users.FirstOrDefault(x => x.UserID == "1");

            if (id != null)
            {
                AdoptionListing listing = dbContext.AdoptionListings.FirstOrDefault(x => x.AdoptionListingID == id);
                if (listing != null)
                {
                    ViewData["listing"] = listing;
                    ViewData["User"] = user;
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult ApplyAdoptionResult([FromForm] AdoptionApplicationBinding application)
        {
            //this boolean can be used in the future if we want to have separate views for acceptance or not
            bool approve = true;
            string id = application.listingId;
            if (id != null)
            {
                AdoptionListing listing = dbContext.AdoptionListings.FirstOrDefault(x => x.AdoptionListingID == id);
                if (listing != null)
                {
                    string species = listing.Species.ToString();
                    ViewData["listing"] = listing;
                    ViewData["reject"] = "Unfortunately your application is not successful";
                    ViewData["description"] = "Not successful";

                    if (species.Equals("Dog"))
                    {
                        if (application.residenceType == Residence.HDB && application.dogsOwned>0)
                        {
                            ViewData["reject"] = "Unfortunately for HDB residence you can only own one dog";
                            approve = false;
                        }
                        else if (application.residenceType == Residence.Private && (int)application.dogsOwned > 2)
                        {
                            ViewData["reject"] = "Unfortunately for private residences you can only own a maximum of three dogs";
                            approve = false;
                        }
                    }
                    else if (species.Equals("Cat")&&application.residenceType== Residence.HDB)
                    {
                        ViewData["reject"] = "Unfortunately you are not allowed to keep cats in HDB residences";
                        approve = false;
                    }
                }
                User user = dbContext.Users.FirstOrDefault(x => x.UserID == "1");
                if (user.AdoptionRequests.Any(o => o.AdoptionListing == listing))
                {
                    ViewData["reject"] = "You have already applied to this listing";
                }
                else if (approve == true)
                {
                    AdoptionRequest adoptionRequest = new AdoptionRequest();
                    adoptionRequest.Description = application.description;
                    adoptionRequest.RequestDate = DateTime.Now;
                    adoptionRequest.User = user;
                    adoptionRequest.AdoptionListing = listing;
                    adoptionRequest.residenceType = application.residenceType;
                    adoptionRequest.dogsOwned = application.dogsOwned;
                    dbContext.AdoptionRequests.Add(adoptionRequest);
                    dbContext.SaveChanges();
                    ViewData["reject"] = "You have successfully applied for an adoption request.";
                    ViewData["description"] = adoptionRequest.Description;
                    ViewData["request"] = adoptionRequest;
                }
            }


            else
            {
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}

