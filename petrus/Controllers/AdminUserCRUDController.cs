using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using petrus.BindingModel;
using petrus.Data;
using petrus.Models;

namespace petrus.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserCRUDController : Controller
    {
        private readonly petrusDb _context;

        public AdminUserCRUDController(petrusDb context)
        {
            _context = context;
        }

        // GET: AdminUserCRUD
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: AdminUserCRUD/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: AdminUserCRUD/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminUserCRUD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,Email")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = userDetails.Name,
                    PhoneNumber = userDetails.PhoneNumber,
                    Email = userDetails.PhoneNumber
                };
                
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDetails);
        }

        // GET: AdminUserCRUD/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(new UserDetails()
            {
                Id = user.Id,
                ConcurrencyStamp =  user.ConcurrencyStamp,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        // POST: AdminUserCRUD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ConcurrencyStamp, Name,PhoneNumber,Email")] UserDetails userDetails)
        {
            if (id != userDetails.Id)
            {
                return NotFound();
            }

            User user = new User()
            {
                Id = userDetails.Id,
                ConcurrencyStamp = userDetails.ConcurrencyStamp,
                Name = userDetails.Name,
                PhoneNumber = userDetails.PhoneNumber,
                Email = userDetails.Email
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userDetails);
        }

        // GET: AdminUserCRUD/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: AdminUserCRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.AdoptionListings.RemoveRange(_context.AdoptionListings.Where(l => l.UserId == id));
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
