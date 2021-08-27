using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using petrus.Services;

namespace petrus.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminApiController : Controller
    {
        private readonly IAdminService _service;

        public AdminApiController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("adoption-speed")]
        public IActionResult AdoptionSpeed()
        {
            return Ok(_service.GetAvgAdoptionSpeed());
        }

        [HttpGet]
        [Route("curr-adoption-speed")]
        public IActionResult CurrAdoptionSpeed()
        {
            return Ok(_service.GetCurrMonthAdoptionSpeed());
        }

        [HttpGet]
        [Route("adoptions-by-species")]
        public IActionResult AdoptionsBySpecies()
        {
            return Ok(_service.GetAdoptionListingsBySpecies());
        }

        [HttpGet]
        [Route("adoptions-by-month")]
        public IActionResult AdoptionsByMonth()
        {
            return Ok(_service.GetAdoptionsByMonth());
        }

        [HttpGet]
        [Route("curr-month-adoption")]
        public IActionResult CurrMonthAdoption()
        {
            return Ok(_service.GetCurrMonthAdoptions());
        }

        [HttpGet]
        [Route("latest-adoptions")]
        public IActionResult LatestAdoptions()
        {
            return Ok(_service.GetLatestAdoptions());
        }

        [HttpGet]
        [Route("slow-adoption-listings")]
        public IActionResult SlowAdoptionListings()
        {
            return Ok(_service.GetSlowAdoptionListings());
        }
    }
}
