using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;
using NationalCountyMeet.Web.Models.Others;
using NationalCountyMeet.Web.Models.ViewModels;

namespace NationalCountyMeet.Web.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PlayersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            ViewBag.totalRegistrations = _context.Players.Count();
            ViewBag.totalMale = _context.Players.Where(m => m.Gender == Gender.Male).Count();
            ViewBag.totalFemale = _context.Players.Where(m => m.Gender == Gender.Female).Count();
            ViewBag.others = _context.Players.Where(m => m.Gender == Gender.Others).Count();
            var applicationDbContext = _context.Players
                //.IgnoreQueryFilters()
                .Include(p => p.County);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Deleted Players
        public async Task<IActionResult> DeletedPlayersList()
        {
            ViewBag.totalRegistrations = _context.Players.Count();
            ViewBag.totalMale = _context.Players.Where(m => m.Gender == Gender.Male).Count();
            ViewBag.totalFemale = _context.Players.Where(m => m.Gender == Gender.Female).Count();
            ViewBag.others = _context.Players.Where(m => m.Gender == Gender.Others).Count();
            var applicationDbContext = _context.Players
                //.Include(p => p.County)
                .IgnoreQueryFilters()
                .Where(d => d.IsDeleted == true);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        // GET: Players/Details/5
        public async Task<IActionResult> RestorePlayerRecord()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> RestorePlayerRecord(int id)
        //{
        //    var player = await _context.Players
        //        .IgnoreQueryFilters()
        //        .FirstOrDefaultAsync(p => p.PlayerId == id);

        //    if (player == null)
        //    {
        //        return NotFound();
        //    }
        //    player.IsDeleted = false;
        //    player.DeletedAt = null;
        //    _context.Update(player);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(DeletedPlayersList));
        //}

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.County)
                .Include(s => s.PlayerStatistics)
                .Include(d => d.PlayerDocument)
                .FirstOrDefaultAsync(m => m.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            var counties = await _context.Counties.ToListAsync();
            var stats = await _context.PlayerStatistics.ToListAsync();
            
            var playerDetailsViewModel = new PlayerDetailsVM
            {
                PlayerID = player.PlayerId,
                FirstName = player.FirstName,
                MiddleName = player.MiddleName,
                LastName = player.LastName,
                Contact = player.Contact,
                Email = player.Email,
                Gender = player.Gender,
                PlaceOfBirth = player.PlaceOfBirth,
                CountyOfOriginCountyId = player.CountyOfOriginCountyId,
                CountyOfOriginCounty = counties.FirstOrDefault(t => t.CountyId == player.CountyOfOriginCountyId)?.CountyName,
                DateOfBirth = player.DateOfBirth,
                Ethnicity = player.Ethnicity,
                CountyId = player.CountyId,
                CountyName = counties.FirstOrDefault(t => t.CountyId == player.CountyId)?.CountyName,
                JerseyNumber = player.JerseyNumber,
                PlayerPosition = player.PlayerPosition,
                HomeAddress = player.HomeAddress,
                HomeContact = player.HomeContact,
                PlayerPhotoUrl = player.PlayerPhotoUrl,
                PlayerDocument = player.PlayerDocument,
                PlayerStatistics = player.PlayerStatistics
            };
            return View(playerDetailsViewModel);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["CountyOfOriginCountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (player.PlayerPhoto != null)
            {
                string folder = "images/player/";
                folder += Guid.NewGuid().ToString() + "_" + player.PlayerPhoto.FileName;
                player.PlayerPhotoUrl = "/" + folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await player.PlayerPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            player.CreatedBy = User.Identity.Name;
            player.CreatedDate = DateTime.Now;  
            _context.Add(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", player.CountyId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> EditPlayerRecord(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", player.CountyId);
            ViewData["CountyOfOriginCountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlayerRecord(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }
            try
            {
                if (player.PlayerPhoto != null)
                {
                    string folder = "images/player/";
                    folder += Guid.NewGuid().ToString() + "_" + player.PlayerPhoto.FileName;
                    player.PlayerPhotoUrl = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await player.PlayerPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                player.ModifiedBy = "test@user.com";
                player.ModifiedDate = DateTime.Now;
                _context.Update(player);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(player.PlayerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));            
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyCapital", player.CountyId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> DeletePlayerRecord(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.County)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("DeletePlayerRecord")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            player.IsDeleted = true;
            player.DeletedAt = DateTime.Now;
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Players/Delete/5
        [HttpGet]
        public async Task<IActionResult> AddPlayerDocuments(int id)
        {
            return View(new PlayerDocumentVM { PlayerId = id });
        }
        // POST: Players/Delete/5
        [HttpPost]
        public async Task<IActionResult> AddPlayerDocuments(PlayerDocumentVM model)
        {
            if (model.Files != null && model.Files.Any())
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents/players");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                foreach (var file in model.Files)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var doc = new PlayerDocument
                    {
                        DocumentName = file.FileName,
                        FilePath = "/uploads/" + fileName,
                        PlayerId = model.PlayerId,
                        Description = model.Description,
                        DocumentType = model.DocumentType
                    };

                    _context.PlayerDocuments.Add(doc);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddPlayerDocuments));
                //return RedirectToAction("Details", "Employees", new { id = model.PlayerId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PlayerDocumentsList()
        {
            var applicationDbContext = _context.PlayerDocuments.Include(p => p.Player);
            return View(await applicationDbContext.ToListAsync());
        }


        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
