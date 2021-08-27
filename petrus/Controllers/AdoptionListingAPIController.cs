using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
        public AdoptionListingAPIController(petrusDb context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
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
            var requests = await dbContext.AdoptionRequests.Where(u => u.User.UserID==adoptionRequestApiBinding.userId).ToListAsync();

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
        [Route("login")]
        public async Task<IActionResult> IsUserValid([FromBody] LoginAttempt loginAttempt)
        {
            User user = await dbContext.Users.FirstOrDefaultAsync(x => x.EmailAddress == loginAttempt.username & x.Password == loginAttempt.password);
            if (user != null)
                return Ok(user);
            return null;
        }

        [HttpPost]
        [Route("application")]
        public async Task<IActionResult> createApplication([FromBody] ApplicationAttempt applicationAttempt)
        {
            AdoptionRequest newRequest = new AdoptionRequest();
            User user= await dbContext.Users.FirstOrDefaultAsync(x => x.UserID == applicationAttempt.userId);
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
