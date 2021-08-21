using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using petrus.Data;

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
            var listings = await dbContext.AdoptionListings.ToListAsync();

            return Ok(listings);
        }

        [HttpGet]
        [Route("{adoptionListingId}/request")]
        public async Task<IActionResult> GetAdoptionRequestByListing(string adoptionListingId)
        {
            var listing = await dbContext.AdoptionRequests.Include(x => x.AdoptionListing).Where(u=>u.AdoptionListing.AdoptionListingID == adoptionListingId).ToListAsync();

            return Ok(listing);
        }




    }
}
