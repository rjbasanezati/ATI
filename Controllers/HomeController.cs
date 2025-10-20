using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ATI_IEC.Data;
using ATI_IEC.Models;
using System.IO;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;

namespace ATI_IEC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 🔹 Load ISS Request Form Page
        public IActionResult ISSRF()
        {
            return View("Dashboard/ISSRF");
        }

        // 🔹 Handle Form Submission & Auto-Download Excel
        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult SubmitISSRequest(RequestFormModel model)
{
    if (!ModelState.IsValid)
        return View("Dashboard/ISSRF", model);

            var filePath = Path.Combine(_env.WebRootPath, "forms", "IEC_Request_Template.xlsx");

    // ✅ Correct license setup for EPPlus 8+
      OfficeOpenXml.ExcelPackage.License.SetNonCommercialOrganization("ATI Davao Region");

    var templatePath = Path.Combine(_env.WebRootPath, "forms", "IEC_Request_Template.xlsx");

    using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
        
    {
        var worksheet = package.Workbook.Worksheets[0];
        worksheet.Cells["C11"].LoadFromCollection(model.RcefRiceSelected);
        worksheet.Cells["I11"].LoadFromCollection(model.CFIDPSelected);
        worksheet.Cells["C21"].LoadFromCollection(model.LivestockSelected);
        worksheet.Cells["I21"].LoadFromCollection(model.HVCDPSelected);
        worksheet.Cells["I31"].LoadFromCollection(model.OrganicSelected);
        worksheet.Cells["C31"].Value = model.OthersSelected;


        worksheet.Cells["E5"].Value = model.Name;
        worksheet.Cells["E6"].Value = model.Office;
        worksheet.Cells["E7"].Value = model.Purpose;
        worksheet.Cells["K5"].Value = model.DateRequested.ToString("MM/dd/yyyy");
        worksheet.Cells["K6"].Value = model.DateOfDistribution.ToString("MM/dd/yyyy");
        worksheet.Cells["K7"].Value = model.TargetRecipients;

        var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        var fileName = $"IEC_Request_{model.Name}_{DateTime.Now:yyyyMMdd}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}
}public IActionResult Main() => View();

// Dashboard pages
public IActionResult ISS() => View("Dashboard/ISS");
public IActionResult PAS() => View("Dashboard/PAS");
public IActionResult AFU() => View("Dashboard/AFU");
public IActionResult CDMS() => View("Dashboard/CDMS");
public IActionResult PMEU() => View("Dashboard/PMEU");
public IActionResult BAC() => View("Dashboard/BAC");

 // ============================
// LOGIN (Index)
// ============================
// GET: Login page
public IActionResult Index()
{
    // Pass a LoginViewModel to the view, not UserReader
    return View(new LoginViewModel());
}

// POST: Handle login
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Index(LoginViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    var user = _context.UserReaders
        .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

    if (user == null)
    {
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(model);
    }

    // Store user info in session
    HttpContext.Session.SetString("UserEmail", user.Email);
    HttpContext.Session.SetString("UserName", user.Name);

    // Redirect to Dashboard
    return RedirectToAction("Main", "Home");
}

        public IActionResult Dashboard()
        {
            var name = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Index");

            ViewBag.UserName = name;
            return View();
        }


// ============================
// USER REGISTRATION
// ============================
public IActionResult Register() => View();

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Register(UserReader model)
{
    if (!ModelState.IsValid)
        return View(model);

    _context.UserReaders.Add(model);
    _context.SaveChanges();

    // Redirect to login after registration
    return RedirectToAction("Index", "Home");
}

        // ============================
        // IEC LIBRARY (User Side)
        // ============================
        public IActionResult IecLibrary(string searchString)
        {
            var iecDocs = _context.IecDocuments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var q = searchString.Trim().ToLower();
                iecDocs = iecDocs.Where(d => d.Title != null && d.Title.ToLower().Contains(q));
            }

            var list = iecDocs.OrderByDescending(d => d.UploadDate).ToList();
            return View(list);
        }

        // ============================
        // IEC UPLOAD (Admin Side)
        // ============================
        public IActionResult UploadIec()
        {
            ViewBag.IecDocuments = _context.IecDocuments
                .OrderByDescending(d => d.UploadDate)
                .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadIec(IecDocument model, IFormFile uploadedFile)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                TempData["Error"] = "Please select a file to upload.";
                return RedirectToAction("UploadIec");
            }

            if (!uploadedFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Only PDF files are allowed.";
                return RedirectToAction("UploadIec");
            }

            var folder = Path.Combine(_env.WebRootPath, "iec");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Path.GetFileName(uploadedFile.FileName);
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(stream);
            }

            model.FilePath = "/iec/" + fileName;
            model.UploadDate = DateTime.Now;

            _context.IecDocuments.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "IEC uploaded successfully!";
            return RedirectToAction("UploadIec");
        }

        [HttpPost]
        public IActionResult DeleteIec(int id)
        {
            var doc = _context.IecDocuments.Find(id);
            if (doc != null)
            {
                var physicalPath = Path.Combine(_env.WebRootPath, doc.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(physicalPath))
                    System.IO.File.Delete(physicalPath);

                _context.IecDocuments.Remove(doc);
                _context.SaveChanges();

                TempData["Success"] = "IEC deleted successfully!";
            }
            else
            {
                TempData["Error"] = "IEC not found.";
            }

            return RedirectToAction("UploadIec");
        }

        // ============================
        // READER LIST (Admin Only)
        // ============================
        public IActionResult ReaderList()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login", "Admin");

            var readers = _context.UserReaders.ToList();
            return View(readers);
        }

        [HttpPost]
        public IActionResult DeleteReader(int id)
        {
            var reader = _context.UserReaders.Find(id);
            if (reader != null)
            {
                _context.UserReaders.Remove(reader);
                _context.SaveChanges();
                TempData["Success"] = "Reader deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Reader not found.";
            }

            return RedirectToAction("ReaderList");
        }
    }
}
