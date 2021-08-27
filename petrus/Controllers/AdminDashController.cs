using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace petrus.Controllers
{
    [Authorize]
    public class AdminDashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
