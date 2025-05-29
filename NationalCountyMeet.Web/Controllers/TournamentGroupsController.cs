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
    public class TournamentGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TournamentGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.TournamentGroups.ToListAsync());
        }

        // GET: TournamentGroups/Details/5
        public async Task<IActionResult> TournamentGroupDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentGroup = await _context.TournamentGroups
                .FirstOrDefaultAsync(m => m.TournamentGroupId == id);
            if (tournamentGroup == null)
            {
                return NotFound();
            }

            return View(tournamentGroup);
        }

        // GET: TournamentGroups/Create
        public IActionResult AddNewTournameGroup()
        {
            return View();
        }

        // POST: TournamentGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewTournameGroup([Bind("TournamentGroupId,GroupName,GroupAlias,Note")] TournamentGroup tournamentGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournamentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournamentGroup);
        }

        // GET: TournamentGroups/Edit/5
        public async Task<IActionResult> EditTournamentGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentGroup = await _context.TournamentGroups.FindAsync(id);
            if (tournamentGroup == null)
            {
                return NotFound();
            }
            return View(tournamentGroup);
        }

        // POST: TournamentGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTournamentGroup(int id, [Bind("TournamentGroupId,GroupName,GroupAlias,Note")] TournamentGroup tournamentGroup)
        {
            if (id != tournamentGroup.TournamentGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentGroupExists(tournamentGroup.TournamentGroupId))
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
            return View(tournamentGroup);
        }

        // GET: TournamentGroups/Delete/5
        public async Task<IActionResult> DeleteTournamentGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentGroup = await _context.TournamentGroups
                .FirstOrDefaultAsync(m => m.TournamentGroupId == id);
            if (tournamentGroup == null)
            {
                return NotFound();
            }

            return View(tournamentGroup);
        }

        // POST: TournamentGroups/Delete/5
        [HttpPost, ActionName("DeleteTournamentGroup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournamentGroup = await _context.TournamentGroups.FindAsync(id);
            if (tournamentGroup != null)
            {
                _context.TournamentGroups.Remove(tournamentGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentGroupExists(int id)
        {
            return _context.TournamentGroups.Any(e => e.TournamentGroupId == id);
        }
    }
}
