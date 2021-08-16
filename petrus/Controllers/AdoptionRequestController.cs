
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using petrus.Data;
using petrus.Models;
using petrus.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using petrus.BindingModel;

namespace petrus.Controllers
{
    public class AdoptionRequestController : Controller
    {
        private readonly petrusDb dbContext;

        public AdoptionRequestController(petrusDb context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<RequestDetailsViewModel> requestDetailsVM = new List<RequestDetailsViewModel>();

            List<AdoptionRequest> adoptionRequestList = await dbContext.AdoptionRequests.Include(x => x.User).Where(u => u.User.UserID == "1").OrderByDescending(t => t.RequestDate).ToListAsync();

            foreach(AdoptionRequest item in adoptionRequestList)
            {
                RequestDetailsViewModel individual = new RequestDetailsViewModel
                {
                    AdoptionRequest = item,
                    AdoptionListing = await dbContext.AdoptionListings.Where(x => x.AdoptionListingID == item.AdoptionListing.AdoptionListingID).ToListAsync()
                };

                requestDetailsVM.Add(individual);

            }

            return View(requestDetailsVM.OrderByDescending(r => r.AdoptionRequest.RequestDate).ToList());
        }

        public IActionResult Delete(string id)
        {
            if (id != null)
            {
                AdoptionRequest request = dbContext.AdoptionRequests.FirstOrDefault(x => x.AdoptionRequestId == id);
                if (request != null)
                {
                    dbContext.AdoptionRequests.Remove(request);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index"); ;
        }

        public IActionResult Details(string id)
        {
            User user = dbContext.Users.FirstOrDefault(x => x.UserID == "1");

            if (id != null)
            {
                AdoptionRequest request = dbContext.AdoptionRequests.FirstOrDefault(x => x.AdoptionRequestId == id);

                if (request != null)
                {
                    ViewData["request"] = request;
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
        public IActionResult Remove([FromForm] string selected)
        {
            if (selected != null)
            {
                AdoptionRequest adoptionRequest =
                    dbContext.AdoptionRequests.FirstOrDefault((x => x.AdoptionRequestId == selected));
                if (adoptionRequest != null)
                {
                    dbContext.AdoptionRequests.Remove(adoptionRequest);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index"); ;
        }




    }
}
