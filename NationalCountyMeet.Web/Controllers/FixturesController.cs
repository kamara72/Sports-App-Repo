using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;
using NationalCountyMeet.Web.Models.ViewModels;

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
            var fixtures = await _context.Fixtures.ToListAsync();
            var counties = await _context.Counties.ToListAsync();
            var officials = await _context.MatchOfficials.ToListAsync();
            var venues = await _context.Venues.ToListAsync();

            var fixtureVM = fixtures.Select(f => new FixtureVM
            {
                FixtureId = f.FixtureId,
                VenueId = f.MatchVenueId,
                VenueName = venues.FirstOrDefault(v => v.MatchVenueId == f.MatchVenueId)?.VenueName,
                HomeId = f.HomeTeamId,
                HomeTeamName = counties.FirstOrDefault(t => t.CountyId == f.HomeTeamId)?.CountyName,
                AwayId = f.AwayTeamId,
                AwayTeamName = counties.FirstOrDefault(t => t.CountyId == f.AwayTeamId)?.CountyName,
                CenterOfficialId = f.CenterOfficialId,
                CenterOfficialName = officials.FirstOrDefault(o => o.MatchOfficialId == f.CenterOfficialId)?.FullName,
                FirstLinesmanId = f.FirstLinesManId,
                FirstLinesmaneName = officials.FirstOrDefault(o => o.MatchOfficialId == f.FirstLinesManId)?.FullName
            }).ToList();
            return View(fixtureVM);
        }

        // GET: Fixtures/Details/5
        public async Task<IActionResult> FixtureDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                //.Include(f => f.MatchVenue)
                .FirstOrDefaultAsync(m => m.FixtureId == id);
            if (fixture == null)
            {
                return NotFound();
            }

            var officials = await _context.MatchOfficials.ToListAsync();
            var fixtures = await _context.Fixtures.ToListAsync();
            var counties = await _context.Counties.ToListAsync();
            var venues = await _context.Venues.ToListAsync();
            var rounds = await _context.TournamentRounds.ToListAsync();

            var fixtureViewModel = new FixtureVM
            {
                FixtureId = fixture.FixtureId,
                MatchDate = fixture.MatchDate,
                RoundId = fixture.TournamentRoundId,
                RoundName = rounds.FirstOrDefault(r => r.TournamentRoundId == fixture.TournamentRoundId)?.Rounds,
                StartTime = fixture.StartTime,
                CenterOfficialId = fixture.CenterOfficialId,
                CenterOfficialName = officials.FirstOrDefault(o => o.MatchOfficialId == fixture.CenterOfficialId)?.FullName,
                FirstLinesmanId = fixture.FirstLinesManId,
                FirstLinesmaneName = officials.FirstOrDefault(o => o.MatchOfficialId == fixture.FirstLinesManId)?.FullName,
                SecondLinesmanId = fixture.SecondLinesManId,
                SecondLinesmanName = officials.FirstOrDefault(o => o.MatchOfficialId == fixture.SecondLinesManId)?.FullName,
                FourthOfficialId = fixture.FourthOfficial,
                FourthOfficialName = officials.FirstOrDefault(o => o.MatchOfficialId == fixture.FourthOfficial)?.FullName,
                VenueId = fixture.MatchVenueId,
                VenueName = venues.FirstOrDefault(v => v.MatchVenueId == fixture.MatchVenueId)?.VenueName,
                VenuePhotoUrl = venues.FirstOrDefault(v => v.MatchVenueId == fixture.MatchVenueId)?.VenuePhotoUrl,
                HomeId = fixture.HomeTeamId,
                HomeTeamName = counties.FirstOrDefault(t => t.CountyId == fixture.HomeTeamId)?.CountyName,
                AwayId = fixture.AwayTeamId,
                AwayTeamName = counties.FirstOrDefault(t => t.CountyId == fixture.AwayTeamId)?.CountyName
            };
            return View(fixtureViewModel);
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
            ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds");
            return View();
        }
         
        // POST: Fixtures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewFixture(Fixture fixture)
        {
            fixture.CreatedBy = "kamara@user.com";
            fixture.CreatedDate = DateTime.Now;
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
            ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds");
            return View(fixture);
        }

        // GET: Fixtures/Edit/5
        public async Task<IActionResult> EditFixture(int? id)
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
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FirstLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["SecondLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FourthOfficial"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["HomeTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["AwayTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds");
            return View(fixture);
        }

        // POST: Fixtures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFixture(int id, Fixture fixture)
        {
            if (id != fixture.FixtureId)
            {
                return NotFound();
            }
            try
            {
                fixture.ModifiedBy = "test@user.com";
                fixture.ModifiedDate = DateTime.Now;
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
            ViewData["MatchVenueId"] = new SelectList(_context.Venues, "MatchVenueId", "VenueName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FirstLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["SecondLinesManId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["CenterOfficialId"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["FourthOfficial"] = new SelectList(_context.MatchOfficials, "MatchOfficialId", "FullName");
            ViewData["HomeTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["AwayTeamId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["TournamentRoundId"] = new SelectList(_context.TournamentRounds, "TournamentRoundId", "Rounds", fixture.TournamentRoundId);
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
