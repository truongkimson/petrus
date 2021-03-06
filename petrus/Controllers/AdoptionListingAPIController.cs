using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using petrus.Areas.Identity.Pages.Account.Manage;
using petrus.Data;
using petrus.BindingModel;
using petrus.Models;

namespace petrus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdoptionListingAPIController : ControllerBase
    {
        private readonly petrusDb dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public AdoptionListingAPIController(petrusDb context, IWebHostEnvironment hostEnvironment, SignInManager<User> signInManager, ILogger<ChangePasswordModel>logger)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var listings = await dbContext.AdoptionListings.OrderBy(o => o.AdoptionListingID.Length).ThenBy(a => a.AdoptionListingID).ToListAsync();

            return Ok(listings);
        }

        [HttpPost]
        [Route("request")]
        public async Task<IActionResult> GetAdoptionRequestByListing([FromBody] AdoptionRequestAPIBinding adoptionRequestApiBinding)
        {
            /*var listing = await dbContext.AdoptionRequests.Include(x => x.AdoptionListing).Where(u => u.AdoptionListing.AdoptionListingID == adoptionListingId && u.User.UserID=).ToListAsync();*/
            var requests = await dbContext.AdoptionRequests.Where(u => u.User.Id==adoptionRequestApiBinding.userId).ToListAsync();

            if (requests != null)
            {
                return Ok(requests);
            }

            return null;
        }

        [HttpGet]
        [Route("request/delete")]
        public async Task<IActionResult> DeleteAdoptionRequestById([FromBody] AdoptionRequestDeleteBinding adoptionRequestDeleteBinding)
        {
            var request = await dbContext.AdoptionRequests.FirstOrDefaultAsync(u => u.AdoptionRequestId == adoptionRequestDeleteBinding.requestId);
            dbContext.Remove(request);
            await dbContext.SaveChangesAsync();
            return Ok("success");
        }


        [HttpPost]
        [Route("request/edit")]
        public async Task<IActionResult> EditAdoptionRequestById([FromBody] AdoptionRequestEditBinding adoptionRequestEditBinding)
        {
            var request = await dbContext.AdoptionRequests.FirstOrDefaultAsync(a =>
                a.AdoptionRequestId == adoptionRequestEditBinding.adoptionRequestId);
            request.dogsOwned = adoptionRequestEditBinding.dogsOwned;
            request.residenceType = (Residence) Enum.Parse(typeof(Residence), adoptionRequestEditBinding.residenceType);
            request.requestStatus = RequestStatus.Pending;
            request.Description = adoptionRequestEditBinding.description;
            await dbContext.SaveChangesAsync();
            return Ok("success");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> IsUserValid([FromBody] LoginAttempt loginAttempt)
        {
            /*User user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginAttempt.username & x.PasswordHash == loginAttempt.password);
            if (user != null)
                return Ok(user);
            return null;*/
            var result = await _signInManager.PasswordSignInAsync(loginAttempt.username, loginAttempt.password, false,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                User user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginAttempt.username);
                return Ok(user);
            }

            return null;

        }

        [HttpPost]
        [Route("application")]
        public async Task<IActionResult> createApplication([FromBody] ApplicationAttempt applicationAttempt)
        {
            AdoptionRequest newRequest = new AdoptionRequest();
            User user= await dbContext.Users.FirstOrDefaultAsync(x => x.Id == applicationAttempt.userId);
            newRequest.User = user;
            AdoptionListing listing = await dbContext.AdoptionListings.FirstOrDefaultAsync(x => x.AdoptionListingID == applicationAttempt.adoptionListingId);
            newRequest.AdoptionListing = listing;
            newRequest.RequestDate = DateTime.Now;
            newRequest.dogsOwned = applicationAttempt.dogsOwned;
            newRequest.residenceType = (Residence)Enum.Parse(typeof(Residence), applicationAttempt.residenceType);
            newRequest.requestStatus = RequestStatus.Pending;
            newRequest.Description = applicationAttempt.description;
            await dbContext.AdoptionRequests.AddAsync(newRequest);
            await dbContext.SaveChangesAsync();
            return Ok("success");
        }

    }
}
