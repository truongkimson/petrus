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
    public class AdoptionRequestController : Controller
    {
        private readonly petrusDb dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdoptionRequestController(petrusDb context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }


        //this is to view all the past transactions
        public IActionResult Index()
        {
            User user = dbContext.Users.FirstOrDefault(x => x.UserID == "1");
            List<AdoptionRequest> adoptionRequests = user.AdoptionRequests.ToList();

            ViewData["AdoptionRequests"] = adoptionRequests;

            return View();
        }

        [HttpPost]
        public IActionResult ViewMore([FromForm] string selected)
        {
            if (selected != null)
            {
                AdoptionRequest adoptionRequest =
                    dbContext.AdoptionRequests.FirstOrDefault((x => x.AdoptionRequestId == selected));
                ViewData["adoptionRequest"] = adoptionRequest;
            }
            else
                return RedirectToAction("Index");
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
                    dbContext.SaveChanges();
                }
                /*else
                {
                    //If already closed
                    return View();
                }*/

                ViewData["adoptionRequest"] = adoptionRequest;
            }
            else
                return RedirectToAction("Index", "AdoptionListing");
            return RedirectToAction("Index", "AdoptionListing");

        }


    }
}
