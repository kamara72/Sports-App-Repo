using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;

namespace NationalCountyMeet.Web.Controllers
{
    public class MatchOfficialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MatchOfficialsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: MatchOfficials
        public async Task<IActionResult> Index()
        {
            return View(await _context.MatchOfficials.ToListAsync());
        }

        // GET: MatchOfficials/Details/5
        public async Task<IActionResult> MatchOfficialDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchOfficial = await _context.MatchOfficials
                .FirstOrDefaultAsync(m => m.MatchOfficialId == id);
            if (matchOfficial == null)
            {
                return NotFound();
            }

            return View(matchOfficial);
        }

        // GET: MatchOfficials/Create
        public IActionResult CreateNewMatchOfficial()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }

        // POST: MatchOfficials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewMatchOfficial(MatchOfficial matchOfficial)
        {
            if (matchOfficial.MatchOfficialPhoto != null)
            {
                string folder = "images/matchofficial/";
                folder += Guid.NewGuid().ToString() + "_" + matchOfficial.MatchOfficialPhoto.FileName;
                matchOfficial.MatchOfficialPhotoUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await matchOfficial.MatchOfficialPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            _context.Add(matchOfficial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));        
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View(matchOfficial);
        }

        // GET: MatchOfficials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchOfficial = await _context.MatchOfficials.FindAsync(id);
            if (matchOfficial == null)
            {
                return NotFound();
            }
            return View(matchOfficial);
        }

        // POST: MatchOfficials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchOfficialId,FirstName,MiddleName,LastName,Contact,Email,Gender,MatchOfficialStatus,PlaceOfBirth,CountyId,Ethnicity,DateOfBirth,PlayerPhotoUrl")] MatchOfficial matchOfficial)
        {
            if (id != matchOfficial.MatchOfficialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchOfficial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchOfficialExists(matchOfficial.MatchOfficialId))
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
            return View(matchOfficial);
        }

        // GET: MatchOfficials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchOfficial = await _context.MatchOfficials
                .FirstOrDefaultAsync(m => m.MatchOfficialId == id);
            if (matchOfficial == null)
            {
                return NotFound();
            }

            return View(matchOfficial);
        }

        // POST: MatchOfficials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchOfficial = await _context.MatchOfficials.FindAsync(id);
            if (matchOfficial != null)
            {
                _context.MatchOfficials.Remove(matchOfficial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchOfficialExists(int id)
        {
            return _context.MatchOfficials.Any(e => e.MatchOfficialId == id);
        }
    }
}
