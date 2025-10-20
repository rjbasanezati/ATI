using Microsoft.AspNetCore.Mvc;
using ATI_IEC.Data;
using ATI_IEC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;

namespace ATI_IEC.Controllers
{
    public class KpsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public KpsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // User: Instructions page
        public IActionResult Instructions()
        {
            return View();
        }

        // User: Submit KPS request
        public IActionResult Submit() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(KpsRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.KpsRequests.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation() => View();

        // Admin: Upload KPS Form
        public IActionResult UploadForm() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadForm(IFormFile uploadedFile)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                TempData["Error"] = "Please select a file to upload.";
                return RedirectToAction("UploadForm");
            }

            if (!uploadedFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Only PDF files are allowed.";
                return RedirectToAction("UploadForm");
            }

            var folder = Path.Combine(_env.WebRootPath, "kpsforms");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Path.GetFileName(uploadedFile.FileName);
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(stream);
            }

            TempData["Success"] = "KPS Form uploaded successfully!";
            return RedirectToAction("UploadForm");
        }
    }
}
