using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using petrus.BindingModel;
using petrus.Data;
using petrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace petrus.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly petrusDb dbContext;
        private readonly ILogger<AccountController> _logger;
        private UserManager<AppUser> _user;

        public AccountController(petrusDb db, ILogger<AccountController> logger)
        {
            this.dbContext = db;
            _logger = logger;
        }
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDetails login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
                 //find users  by emailaddress
                 var user = await _user.FindByNameAsync(login.EmailAddress);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(login.EmailAddress), $"Email {login.EmailAddress} not exists");
                }
                else
                {
                if (await _user.CheckPasswordAsync(user, login.Password))
                {
                    AuthenticationProperties props = null;
                    if (login.RememberMe)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };
                    }
                    await _sign.SignInAsync(user, props);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("~/");
                }
                ModelState.AddModelError(nameof(login.Password), "Wrong password");
            }
            return View(login);
        }


                
                    
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Register(LoginDetails model)
        {
            
            var user = new AppUser { LoginName = model.EmailAddress};
            //bind user and password to db
            var result = await _user.CreateAsync(user, model.Password);
            return Json(result);
        }

        public IActionResult Denied()
        {
            return Content("Not implemented yet");
        }
    }
}
