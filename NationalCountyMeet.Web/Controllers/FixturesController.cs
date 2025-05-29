using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;

namespace NationalCountyMeet.Web.Controllers
{
    public class FixturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FixturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fixtures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fixtures.Include(f => f.MatchVenue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Test
        //public async Task<IActionResult> FixtureList()
        //{
        //    var applicationDbContext = _context.Fixtures.Include(f => f.MatchVenue);
        //    var sql = from c in _context.Fixtures join o in _context.Counties on new
        //    {
        //        c.FixtureId,


        //    }
        //    return View(await applicationDbContext.ToListAsync());
        //}



        // GET: Fixtures/Details/5
        public async Task<IActionResult> FixtureDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                .Include(f => f.MatchVenue)
                .FirstOrDefaultAsync(m => m.FixtureId == id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(fixture);
        }

        // GET: Fixtures/Create
        public IActionResult CreateNewFixture()
        {
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FirstLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["SecondLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FourthOfficial"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["HomeTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["AwayTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }
         
        // POST: Fixtures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewFixture(Fixture fixture)
        {
            _context.Add(fixture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName", fixture.MatchVenueId);
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FirstLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["SecondLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FourthOfficial"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["HomeTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["AwayTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View(fixture);
        }

        // GET: Fixtures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var fixture = await _context.Fixtures.FindAsync(id);
            if (fixture == null)
            {
                return NotFound();
            }
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "Location", fixture.MatchVenueId);
            return View(fixture);
        }

        // POST: Fixtures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FixtureId,MatchDate,StartTime,CenterOfficialId,FirstLinesManId,SecondLinesManId,FourthOfficial,HomeTeamId,AwayTeamId,MatchVenueId")] Fixture fixture)
        {
            if (id != fixture.FixtureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixtureExists(fixture.FixtureId))
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
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "Location", fixture.MatchVenueId);
            return View(fixture);
        }

        // GET: Fixtures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                .Include(f => f.MatchVenue)
                .FirstOrDefaultAsync(m => m.FixtureId == id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(fixture);
        }

        // POST: Fixtures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixture = await _context.Fixtures.FindAsync(id);
            if (fixture != null)
            {
                _context.Fixtures.Remove(fixture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixtureExists(int id)
        {
            return _context.Fixtures.Any(e => e.FixtureId == id);
        }
    }
}
