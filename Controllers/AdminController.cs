using Microsoft.AspNetCore.Mvc;
using ATI_IEC.Data;
using ATI_IEC.Models;

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
            return View(new IecDocument()); // single object for form binding
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
                doc.UploadDate = DateTime.Now;
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
        public IActionResult KpsList()
{
    if (HttpContext.Session.GetString("IsAdmin") != "true")
        return RedirectToAction("Login");

    var requests = _context.KpsRequests.ToList();
    return View(requests);
}

    }
}
