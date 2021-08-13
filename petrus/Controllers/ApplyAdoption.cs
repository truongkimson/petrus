using Microsoft.AspNetCore.Mvc;
using petrus.Data;
using petrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Controllers
{
    public class ApplyAdoption : Controller
    {
        private readonly petrusDb dbContext;

        public ApplyAdoption(petrusDb context)
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
    }
}
