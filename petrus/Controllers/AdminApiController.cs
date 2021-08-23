using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminApiController : Controller
    {
        [HttpGet]
        [Route("adoption-speed")]
        public IActionResult AdoptionSpeed()
        {
            return Ok("Hello world");
        }

        [HttpGet]
        [Route("adoptions-by-species")]
        public IActionResult AdoptionsBySpecies()
        {
            return Ok("Hello world - Speices");
        }

        [HttpGet]
        [Route("adoptions-by-month")]
        public IActionResult AdoptionsByMonth()
        {
            return Ok("Hello world - Monthly");
        }
    }
}
