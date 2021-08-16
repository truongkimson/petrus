using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using petrus.Data;
using petrus.Models;
using petrus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
    }
}
