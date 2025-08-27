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
    public class PlayerStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayerStatistics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlayerStatistics.Include(p => p.Match).Include(p => p.Player);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlayerStatistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerStatistic = await _context.PlayerStatistics
                .Include(p => p.Match)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerStatisticId == id);
            if (playerStatistic == null)
            {
                return NotFound();
            }

            return View(playerStatistic);
        }

        // GET: PlayerStatistics/Create
        public IActionResult AddNewPlayerStatistics()
        {
            //ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital");
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId");
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName");
            return View();
        }

        // POST: PlayerStatistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewPlayerStatistics(PlayerStatistic playerStatistic)
        {
            playerStatistic.CreatedBy = "alexie@user.com";
            playerStatistic.CreatedDate = DateTime.Now;
            _context.Add(playerStatistic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));        
            //ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", playerStatistic.CountyId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", playerStatistic.MatchId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", playerStatistic.PlayerId);
            return View(playerStatistic);
        }

        // GET: PlayerStatistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerStatistic = await _context.PlayerStatistics.FindAsync(id);
            if (playerStatistic == null)
            {
                return NotFound();
            }
            //ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", playerStatistic.CountyId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", playerStatistic.MatchId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "Contact", playerStatistic.PlayerId);
            return View(playerStatistic);
        }

        // POST: PlayerStatistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerStatisticId,PlayerId,MatchId,Saves,MinutesPlayed,PassesCompleted,Goals,Asists,Shots,ShotsOnTarget,Tackles,RedCards,YellowCards,CountyId")] PlayerStatistic playerStatistic)
        {
            if (id != playerStatistic.PlayerStatisticId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    playerStatistic.ModifiedBy = "kam@user.com";
                    playerStatistic.ModifiedDate = DateTime.UtcNow;
                    _context.Update(playerStatistic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerStatisticExists(playerStatistic.PlayerStatisticId))
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
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", playerStatistic.MatchId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "Contact", playerStatistic.PlayerId);
            return View(playerStatistic);
        }

        // GET: PlayerStatistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerStatistic = await _context.PlayerStatistics
                .Include(p => p.Match)
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerStatisticId == id);
            if (playerStatistic == null)
            {
                return NotFound();
            }

            return View(playerStatistic);
        }

        // POST: PlayerStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerStatistic = await _context.PlayerStatistics.FindAsync(id);
            if (playerStatistic != null)
            {
                _context.PlayerStatistics.Remove(playerStatistic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerStatisticExists(int id)
        {
            return _context.PlayerStatistics.Any(e => e.PlayerStatisticId == id);
        }
    }
}
