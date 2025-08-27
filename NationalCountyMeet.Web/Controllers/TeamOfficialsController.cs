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
using NationalCountyMeet.Web.Models.Others;

namespace NationalCountyMeet.Web.Controllers
{
    public class TeamOfficialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamOfficialsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: TeamOfficials
        public async Task<IActionResult> TeamOfficialsList()
        {
            ViewBag.totalRegistrations = _context.TeamOfficials.Count();
            ViewBag.totalMale = _context.TeamOfficials.Where(m => m.Gender == Gender.Male).Count();
            ViewBag.totalFemale = _context.TeamOfficials.Where(m => m.Gender == Gender.Female).Count();
            ViewBag.others = _context.TeamOfficials.Where(m => m.Gender == Gender.Others).Count();
            var applicationDbContext = _context.TeamOfficials.Include(t => t.County);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamOfficials/Details/5
        public async Task<IActionResult> TeamOfficialsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamOfficial = await _context.TeamOfficials
                .Include(t => t.County)
                .FirstOrDefaultAsync(m => m.TeamOfficialId == id);
            if (teamOfficial == null)
            {
                return NotFound();
            }

            return View(teamOfficial);
        }

        // GET: TeamOfficials/Create
        public IActionResult CreateNewTeamOfficial()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }

        // POST: TeamOfficials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewTeamOfficial(TeamOfficial teamOfficial)
        {
            if (teamOfficial.TeamOfficialPhoto != null)
            {
                string folder = "images/teamofficial/";
                folder += Guid.NewGuid().ToString() + "_" + teamOfficial.TeamOfficialPhoto.FileName;
                teamOfficial.TeamOfficialPhotoUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await teamOfficial.TeamOfficialPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            teamOfficial.CreatedBy = "kamara@user.com";
            teamOfficial.CreatedDate = DateTime.Now;
            _context.Add(teamOfficial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TeamOfficialsList));        
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", teamOfficial.CountyId);
            return View(teamOfficial);
        }

        // GET: TeamOfficials/Edit/5
        public async Task<IActionResult> EditTeamOfficial(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamOfficial = await _context.TeamOfficials.FindAsync(id);
            if (teamOfficial == null)
            {
                return NotFound();
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", teamOfficial.CountyId);
            return View(teamOfficial);
        }

        // POST: TeamOfficials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeamOfficial(int id, TeamOfficial teamOfficial)
        {
            if (id != teamOfficial.TeamOfficialId)
            {
                return NotFound();
            }
            try
            {
                teamOfficial.ModifiedBy = "kamara@user.com";
                teamOfficial.ModifiedDate = DateTime.Now;
                _context.Update(teamOfficial);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamOfficialExists(teamOfficial.TeamOfficialId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }            
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", teamOfficial.CountyId);
            return View(teamOfficial);
        }

        // GET: TeamOfficials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamOfficial = await _context.TeamOfficials
                .Include(t => t.County)
                .FirstOrDefaultAsync(m => m.TeamOfficialId == id);
            if (teamOfficial == null)
            {
                return NotFound();
            }

            return View(teamOfficial);
        }

        // POST: TeamOfficials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamOfficial = await _context.TeamOfficials.FindAsync(id);
            if (teamOfficial != null)
            {
                _context.TeamOfficials.Remove(teamOfficial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamOfficialExists(int id)
        {
            return _context.TeamOfficials.Any(e => e.TeamOfficialId == id);
        }
    }
}
