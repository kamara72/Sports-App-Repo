using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using NationalCountyMeet.Web.Data;
using NationalCountyMeet.Web.Models;
using NationalCountyMeet.Web.Models.ViewModels;

namespace NationalCountyMeet.Web.Controllers
{
    public class PlayerDocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlayerDocumentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PlayerDocuments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlayerDocuments.Include(p => p.Player);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlayerDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDocument = await _context.PlayerDocuments
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.DocumentId == id);
            if (playerDocument == null)
            {
                return NotFound();
            }

            return View(playerDocument);
        }

        // GET: PlayerDocuments/Create
        [HttpGet]
        public IActionResult AddNewPlayerDocument()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName");
            return View();
        }

        // POST: PlayerDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewPlayerDocument(PlayerDocumentVM model)
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
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PlayerDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDocument = await _context.PlayerDocuments.FindAsync(id);
            if (playerDocument == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "Contact", playerDocument.PlayerId);
            return View(playerDocument);
        }

        // POST: PlayerDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentId,PlayerId,DocumentName,Description,FilePath,DocumentType")] PlayerDocument playerDocument)
        {
            if (id != playerDocument.DocumentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerDocumentExists(playerDocument.DocumentId))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "Contact", playerDocument.PlayerId);
            return View(playerDocument);
        }

        // GET: PlayerDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDocument = await _context.PlayerDocuments
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.DocumentId == id);
            if (playerDocument == null)
            {
                return NotFound();
            }

            return View(playerDocument);
        }

        // POST: PlayerDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerDocument = await _context.PlayerDocuments.FindAsync(id);
            if (playerDocument != null)
            {
                _context.PlayerDocuments.Remove(playerDocument);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult DownloadPlayerDocument(string documentName)
        {
            if (string.IsNullOrEmpty(documentName))
                return NotFound();

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "documents/Players/", documentName);

            if (!System.IO.File.Exists(path))
                return NotFound();

            var contentType = GetContentType(path);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, contentType, documentName);
        }

        private string GetContentType(string path)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        //public async Task<IActionResult> DownloadPlayerDocument(int id)
        //{
        //    var document = await _context.PlayerDocuments.FindAsync(id);
        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    //string folder = "images/player/";
        //    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "~/documents/Players/");
        //    //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, document.FilePath.TrimStart('/'), folder);

        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound("File not found on server.");
        //    }

        //    var contentType = "application/octet-stream"; // fallback
        //    var extension = Path.GetExtension(filePath).ToLowerInvariant();
        //    if (extension == ".pdf") contentType = "application/pdf";
        //    else if (extension == ".doc" || extension == ".docx") contentType = "application/vnd.ms-word";
        //    else if (extension == ".xls" || extension == ".xlsx") contentType = "application/vnd.ms-excel";

        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    return File(fileBytes, contentType, document.DocumentName);
        //}

        private bool PlayerDocumentExists(int id)
        {
            return _context.PlayerDocuments.Any(e => e.DocumentId == id);
        }
    }
}
