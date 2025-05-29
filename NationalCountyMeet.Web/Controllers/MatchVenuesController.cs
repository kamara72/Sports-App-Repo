using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;

namespace NationalCountyMeet.Web.Controllers
{
    public class MatchVenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MatchVenuesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: MatchVenues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        // GET: MatchVenues/Details/5
        public async Task<IActionResult> MatchVenueDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchVenue = await _context.Venues
                .FirstOrDefaultAsync(m => m.MatchVenueId == id);
            if (matchVenue == null)
            {
                return NotFound();
            }

            return View(matchVenue);
        }

        // GET: MatchVenues/Create
        public IActionResult AddNewMatchVenue()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }

        // POST: MatchVenues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewMatchVenue(MatchVenue matchVenue)
        {
            if (matchVenue.VenuePhoto != null)
            {
                string folder = "images/venue/";
                folder += Guid.NewGuid().ToString() + "_" + matchVenue.VenuePhoto.FileName;
                matchVenue.VenuePhotoUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await matchVenue.VenuePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            _context.Add(matchVenue);
            await _context.SaveChangesAsync();
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyId");
            return RedirectToAction(nameof(Index));
        }

        // GET: MatchVenues/Edit/5
        public async Task<IActionResult> EditVenueRecord(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchVenue = await _context.Venues.FindAsync(id);
            if (matchVenue == null)
            {
                return NotFound();
            }
            return View(matchVenue);
        }

        // POST: MatchVenues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVenueRecord(int id, [Bind("MatchVenueId,VenueName,Location,Capacity")] MatchVenue matchVenue)
        {
            if (id != matchVenue.MatchVenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchVenue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchVenueExists(matchVenue.MatchVenueId))
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
            return View(matchVenue);
        }

        // GET: MatchVenues/Delete/5
        public async Task<IActionResult> DeleteMatchVenue(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchVenue = await _context.Venues
                .FirstOrDefaultAsync(m => m.MatchVenueId == id);
            if (matchVenue == null)
            {
                return NotFound();
            }

            return View(matchVenue);
        }

        // POST: MatchVenues/Delete/5
        [HttpPost, ActionName("DeleteMatchVenue")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchVenue = await _context.Venues.FindAsync(id);
            if (matchVenue != null)
            {
                _context.Venues.Remove(matchVenue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchVenueExists(int id)
        {
            return _context.Venues.Any(e => e.MatchVenueId == id);
        }
    }
}
