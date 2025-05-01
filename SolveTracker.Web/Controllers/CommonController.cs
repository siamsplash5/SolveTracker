using Microsoft.AspNetCore.Mvc;

namespace SolveTracker.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult SessionExpired()
        {
            return View();
        }
    }
}
