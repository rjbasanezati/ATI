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
        private readonly Cloudinary _cloudinary;

        public AdminController(ApplicationDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
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
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = "ati_iec" // all files go into a single Cloudinary folder
                    };
                    var uploadResult = _cloudinary.Upload(uploadParams);

                    doc.FilePath = uploadResult.SecureUrl.ToString(); // store Cloudinary URL
                    doc.UploadDate = DateTime.Now;
                    if (string.IsNullOrEmpty(doc.Description))
                        doc.Description = "";

                    _context.IecDocuments.Add(doc);
                    _context.SaveChanges();
                }

                TempData["Success"] = "IEC uploaded successfully to Cloudinary!";
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

            try
            {
                // Extract public_id from URL to delete from Cloudinary
                var publicId = Path.GetFileNameWithoutExtension(new Uri(doc.FilePath).AbsolutePath);
                var deletionParams = new DeletionParams(publicId);
                _cloudinary.Destroy(deletionParams);
            }
            catch
            {
                // ignore if deletion fails
            }

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
