﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.Controllers
{
    public class AdoptionRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}