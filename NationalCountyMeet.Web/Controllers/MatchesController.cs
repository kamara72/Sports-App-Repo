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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matches.Include(m => m.Fixture).Include(m => m.MatchVenue); //.Include(m => m.TournamentRound);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Fixture)
                .Include(m => m.MatchVenue)
                // .Include(m => m.TournamentRound)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult NewMatchResult()
        {
            ViewData["FixtureId"] = new SelectList(_context.Fixtures, "FixtureId", "MatchFixture");
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName");
            //ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewMatchResult(Match match)
        {
            match.CreatedDate = DateTime.Now;
            match.CreatedBy = "john@user.com";
            _context.Add(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["FixtureId"] = new SelectList(_context.Fixtures, "FixtureId", "MatchFixture", match.FixtureId);
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName", match.MatchVenueId);
            //ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds", match.TournamentRoundId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> EditMatchResult(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["FixtureId"] = new SelectList(_context.Fixtures, "FixtureId", "MatchFixture", match.FixtureId);
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName", match.MatchVenueId);
            //ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds", match.TournamentRoundId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMatchResult(int id, Match match)
        {
            if (id != match.MatchId)
            {
                return NotFound();
            }
            try
            {
                match.ModifiedDate = DateTime.Now;
                match.ModifiedBy = "musu@user.com";
                _context.Update(match);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(match.MatchId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            ViewData["FixtureId"] = new SelectList(_context.Fixtures, "FixtureId", "MatchFixture", match.FixtureId);
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName", match.MatchVenueId);
            // ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds", match.TournamentRoundId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Fixture)
                .Include(m => m.MatchVenue)
                // .Include(m => m.TournamentRound)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
