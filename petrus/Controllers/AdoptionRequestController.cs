
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
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

            foreach (AdoptionRequest item in adoptionRequestList)
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

        [HttpPost]
        public IActionResult Accept([FromForm] string selected)
        {
            if (selected != null)
            {
                AdoptionRequest adoptionRequest =
                    dbContext.AdoptionRequests.FirstOrDefault((x => x.AdoptionRequestId == selected));
                AdoptionListing adoptionListing = adoptionRequest.AdoptionListing;
                if (adoptionListing.ApplicationStatus == ApplicationStatus.Open)
                {
                    adoptionListing.ApplicationStatus = ApplicationStatus.Closed;
                    adoptionListing.AcceptedRequest = selected;
                    adoptionRequest.requestStatus = RequestStatus.Accepted;
                    adoptionRequest.OutcomeDateTime = DateTime.Now;
                    dbContext.SaveChanges();
                    ViewData["result"] = "You have successfully accepted the request.";
                    ViewData["list"] = adoptionRequest;
                }
                else
                {
                    ViewData["result"] = "You have already accepted this request.";
                    return View();
                }

                ViewData["adoptionRequest"] = adoptionRequest;
            }
            else
                return RedirectToAction("Index", "AdoptionListing");

            return View();

        }

        [HttpPost]
        public IActionResult Reject([FromForm] string selected)
        {
            if (selected != null)
            {
                AdoptionRequest adoptionRequest =
                    dbContext.AdoptionRequests.FirstOrDefault((x => x.AdoptionRequestId == selected));
                AdoptionListing adoptionListing = adoptionRequest.AdoptionListing;
                if (adoptionListing.ApplicationStatus == ApplicationStatus.Open)
                {
                    adoptionRequest.requestStatus = RequestStatus.Rejected;
                    dbContext.SaveChanges();
                    ViewData["result"] = "You have successfully rejected the request.";
                    ViewData["list"] = adoptionRequest;
                }
                else
                {
                    ViewData["result"] = "You have already accepted this request.";
                    return View("Accept");
                }

                ViewData["adoptionRequest"] = adoptionRequest;
            }
            else
                return RedirectToAction("Index", "AdoptionListing");

            return View("Accept");

        }

        [HttpPost]
        public IActionResult UpdateAdoptionRequest([FromForm] AdoptionApplicationBinding application)
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
                    ViewData["reject"] = "Unfortunately, your application is not updated successfully.";
                    ViewData["description"] = "Not successful";

                    if (species.Equals("Dog"))
                    {
                        if (application.residenceType == Residence.HDB && application.dogsOwned > 0)
                        {
                            ViewData["reject"] = "For HDB residence, you can only own one dog.";
                            approve = false;
                        }
                        else if (application.residenceType == Residence.Private && (int)application.dogsOwned > 2)
                        {
                            ViewData["reject"] = "For private residence, you can only own a maximum of three dogs.";
                            approve = false;
                        }
                    }
                    else if (species.Equals("Cat") && application.residenceType == Residence.HDB)
                    {
                        ViewData["reject"] = "You are not allowed to keep cats in HDB residences.";
                        approve = false;
                    }
                }

                if (approve == true)
                {
                    AdoptionRequest adoptionRequest = dbContext.AdoptionRequests.FirstOrDefault(x => x.AdoptionRequestId == id);
                    adoptionRequest.Description = application.description;
                    adoptionRequest.AdoptionListing = listing;
                    adoptionRequest.residenceType = application.residenceType;
                    adoptionRequest.dogsOwned = application.dogsOwned;
                    dbContext.AdoptionRequests.Update(adoptionRequest);
                    dbContext.SaveChanges();
                    ViewData["reject"] = "You have successfully updated your adoption request.";
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
