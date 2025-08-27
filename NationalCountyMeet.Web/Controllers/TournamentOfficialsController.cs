using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;

namespace NationalCountyMeet.Web.Controllers
{
    public class TournamentOfficialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TournamentOfficialsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: TournamentOfficials
        public async Task<IActionResult> TournamentOfficialList()
        {
            var applicationDbContext = _context.TournamentOfficials.Include(t => t.County);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TournamentOfficials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentOfficial = await _context.TournamentOfficials
                .Include(t => t.County)
                .FirstOrDefaultAsync(m => m.TournamentOfficialId == id);
            if (tournamentOfficial == null)
            {
                return NotFound();
            }

            return View(tournamentOfficial);
        }

        // GET: TournamentOfficials/Create
        public IActionResult AddNewTournamentOfficials()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }

        // POST: TournamentOfficials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewTournamentOfficials(TournamentOfficial tournamentOfficial)
        {
            if (tournamentOfficial.TournamentOfficialPhoto != null)
            {
                string folder = "images/tournamentofficial/";
                folder += Guid.NewGuid().ToString() + "_" + tournamentOfficial.TournamentOfficialPhoto.FileName;
                tournamentOfficial.TournamentOfficialPhotoUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await tournamentOfficial.TournamentOfficialPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            tournamentOfficial.CreatedDate = DateTime.Now;
            tournamentOfficial.CreatedBy = "thomas@user.com";
            _context.Add(tournamentOfficial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TournamentOfficialList));        
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", tournamentOfficial.CountyId);
            return View(tournamentOfficial);
        }

        // GET: TournamentOfficials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentOfficial = await _context.TournamentOfficials.FindAsync(id);
            if (tournamentOfficial == null)
            {
                return NotFound();
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", tournamentOfficial.CountyId);
            return View(tournamentOfficial);
        }

        // POST: TournamentOfficials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TournamentOfficialId,FirstName,MiddleName,LastName,Contact,Email,Gender,PositionId,PlaceOfBirth,CountyId,Ethnicity,DateOfBirth,TournamentOfficialPhotoUrl")] TournamentOfficial tournamentOfficial)
        {
            if (id != tournamentOfficial.TournamentOfficialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tournamentOfficial.ModifiedBy = "john@doe.com";
                    tournamentOfficial.ModifiedDate = DateTime.Now;
                    _context.Update(tournamentOfficial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentOfficialExists(tournamentOfficial.TournamentOfficialId))
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
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", tournamentOfficial.CountyId);
            return View(tournamentOfficial);
        }

        // GET: TournamentOfficials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentOfficial = await _context.TournamentOfficials
                .Include(t => t.County)
                .FirstOrDefaultAsync(m => m.TournamentOfficialId == id);
            if (tournamentOfficial == null)
            {
                return NotFound();
            }

            return View(tournamentOfficial);
        }

        // POST: TournamentOfficials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournamentOfficial = await _context.TournamentOfficials.FindAsync(id);
            if (tournamentOfficial != null)
            {
                _context.TournamentOfficials.Remove(tournamentOfficial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentOfficialExists(int id)
        {
            return _context.TournamentOfficials.Any(e => e.TournamentOfficialId == id);
        }
    }
}
