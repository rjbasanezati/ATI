using Microsoft.AspNetCore.Mvc;
using ATI_IEC.Data;
using ATI_IEC.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ATI_IEC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // Manage page: shows both tabs and lists
        public IActionResult ManageFITS()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            ViewBag.FitsCenters = _context.FitsCenters.OrderByDescending(f => f.LaunchedDate).ToList();
            ViewBag.FitsKiosks = _context.FitsKiosks.OrderByDescending(k => k.LaunchedDate).ToList();
            return View();
        }

        // ---------- FITS CENTER CRUD ----------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFitsCenter(FitsCenter model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");
            if (!ModelState.IsValid)
                return RedirectToAction("ManageFITS");

            model.LaunchedDate = DateTime.SpecifyKind(model.LaunchedDate, DateTimeKind.Utc);
            _context.FitsCenters.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "FITS Center added.";
            return RedirectToAction("ManageFITS");
        }

        public IActionResult EditFitsCenter(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var item = _context.FitsCenters.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFitsCenter(FitsCenter model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            if (!ModelState.IsValid) return View(model);

            var dbItem = _context.FitsCenters.Find(model.Id);
            if (dbItem == null) return NotFound();

            dbItem.CenterName = model.CenterName;
            dbItem.LaunchedDate = DateTime.SpecifyKind(model.LaunchedDate, DateTimeKind.Utc);
            dbItem.Status = model.Status;
            dbItem.Address = model.Address;
            dbItem.InCharge = model.InCharge;
            dbItem.Email = model.Email;

            _context.SaveChanges();
            TempData["Success"] = "FITS Center updated.";
            return RedirectToAction("ManageFITS");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFitsCenter(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var item = _context.FitsCenters.Find(id);
            if (item != null)
            {
                _context.FitsCenters.Remove(item);
                _context.SaveChanges();
                TempData["Success"] = "FITS Center deleted.";
            }
            return RedirectToAction("ManageFITS");
        }

        // ---------- FITS KIOSK CRUD ----------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFitsKiosk(FitsKiosk model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");
            if (!ModelState.IsValid)
                return RedirectToAction("ManageFITS");

            model.LaunchedDate = DateTime.SpecifyKind(model.LaunchedDate, DateTimeKind.Utc);
            _context.FitsKiosks.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "FITS Kiosk added.";
            return RedirectToAction("ManageFITS");
        }

        public IActionResult EditFitsKiosk(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var item = _context.FitsKiosks.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFitsKiosk(FitsKiosk model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");
            if (!ModelState.IsValid) return View(model);

            var dbItem = _context.FitsKiosks.Find(model.Id);
            if (dbItem == null) return NotFound();

            dbItem.KioskName = model.KioskName;
            dbItem.Address = model.Address;
            dbItem.LaunchedDate = DateTime.SpecifyKind(model.LaunchedDate, DateTimeKind.Utc);

            _context.SaveChanges();
            TempData["Success"] = "FITS Kiosk updated.";
            return RedirectToAction("ManageFITS");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFitsKiosk(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var item = _context.FitsKiosks.Find(id);
            if (item != null)
            {
                _context.FitsKiosks.Remove(item);
                _context.SaveChanges();
                TempData["Success"] = "FITS Kiosk deleted.";
            }
            return RedirectToAction("ManageFITS");
        }    

//__________________________________________________________________________________________________________________
        // ------------------- LOGIN -------------------
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username == "ISS_ATI_Admin" && password == "Admin2025")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        // ------------------- LOGOUT -------------------
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Index", "Home");
        }

        // ------------------- DASHBOARD -------------------
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            return View();
        }

// ------------------- UPLOAD IEC -------------------
public IActionResult UploadIec()
{
    if (HttpContext.Session.GetString("IsAdmin") != "true")
        return RedirectToAction("Login");

    ViewBag.IecDocuments = _context.IecDocuments.ToList();
    return View(new IecDocument());
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult UploadIec(IecDocument doc)
{
    if (HttpContext.Session.GetString("IsAdmin") != "true")
        return RedirectToAction("Login");

    var file = Request.Form.Files["uploadedFile"];
    if (file == null || file.Length == 0)
    {
        TempData["Error"] = "Please select a file.";
        return RedirectToAction("UploadIec");
    }

    try
    {
        var uploads = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        var filePath = Path.Combine(uploads, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        doc.FilePath = "/uploads/" + file.FileName;

        // ✅ Must be UTC for PostgreSQL
        doc.UploadDate = DateTime.UtcNow;

        if (string.IsNullOrEmpty(doc.Description))
            doc.Description = "";

        _context.IecDocuments.Add(doc);
        _context.SaveChanges();

        TempData["Success"] = "IEC uploaded successfully!";
    }
    catch (Exception ex)
    {
        TempData["Error"] = "Error uploading file: " + ex.Message;
    }

    return RedirectToAction("UploadIec");
}



        // ------------------- DELETE IEC -------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteIec(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var doc = _context.IecDocuments.Find(id);
            if (doc == null)
            {
                TempData["Error"] = "IEC not found.";
                return RedirectToAction("UploadIec");
            }

            var filePath = Path.Combine(_env.WebRootPath, doc.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _context.IecDocuments.Remove(doc);
            _context.SaveChanges();

            TempData["Success"] = "IEC deleted successfully!";
            return RedirectToAction("UploadIec");
        }

        // ------------------- READER LIST -------------------
        public IActionResult ReaderList()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var readers = _context.UserReaders.ToList();
            return View(readers);
        }
    }
}
