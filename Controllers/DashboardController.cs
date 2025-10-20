using Microsoft.AspNetCore.Mvc;

namespace ATI_IEC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View(); // User dashboard
        }
    }
}
