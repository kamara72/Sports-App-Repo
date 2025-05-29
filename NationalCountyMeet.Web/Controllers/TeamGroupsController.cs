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
    public class TeamGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeamGroups.Include(t => t.County).Include(t => t.Tournament).Include(t => t.TournamentGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamGroup = await _context.TeamGroups
                .Include(t => t.County)
                .Include(t => t.Tournament)
                .Include(t => t.TournamentGroup)
                .FirstOrDefaultAsync(m => m.TeamGroupId == id);
            if (teamGroup == null)
            {
                return NotFound();
            }

            return View(teamGroup);
        }

        // GET: TeamGroups/Create
        public IActionResult AddTeamToGroup()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentYear");
            ViewData["TournamentGroupId"] = new SelectList(_context.TournamentGroups, "TournamentGroupId", "GroupName");
            return View();
        }

        // POST: TeamGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeamToGroup(TeamGroup teamGroup)
        {
            _context.Add(teamGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", teamGroup.CountyId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentYear", teamGroup.TournamentId);
            ViewData["TournamentGroupId"] = new SelectList(_context.TournamentGroups, "TournamentGroupId", "GroupName", teamGroup.TournamentGroupId);
            return View(teamGroup);
        }

        // GET: TeamGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamGroup = await _context.TeamGroups.FindAsync(id);
            if (teamGroup == null)
            {
                return NotFound();
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", teamGroup.CountyId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentId", teamGroup.TournamentId);
            ViewData["TournamentGroupId"] = new SelectList(_context.TournamentGroups, "TournamentGroupId", "GroupAlias", teamGroup.TournamentGroupId);
            return View(teamGroup);
        }

        // POST: TeamGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamGroupId,TournamentGroupId,TournamentId,CountyId,Note")] TeamGroup teamGroup)
        {
            if (id != teamGroup.TeamGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamGroupExists(teamGroup.TeamGroupId))
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
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", teamGroup.CountyId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "TournamentId", teamGroup.TournamentId);
            ViewData["TournamentGroupId"] = new SelectList(_context.TournamentGroups, "TournamentGroupId", "GroupAlias", teamGroup.TournamentGroupId);
            return View(teamGroup);
        }

        // GET: TeamGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamGroup = await _context.TeamGroups
                .Include(t => t.County)
                .Include(t => t.Tournament)
                .Include(t => t.TournamentGroup)
                .FirstOrDefaultAsync(m => m.TeamGroupId == id);
            if (teamGroup == null)
            {
                return NotFound();
            }

            return View(teamGroup);
        }

        // POST: TeamGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamGroup = await _context.TeamGroups.FindAsync(id);
            if (teamGroup != null)
            {
                _context.TeamGroups.Remove(teamGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamGroupExists(int id)
        {
            return _context.TeamGroups.Any(e => e.TeamGroupId == id);
        }
    }
}
