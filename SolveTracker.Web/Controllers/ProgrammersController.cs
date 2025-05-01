using Microsoft.AspNetCore.Mvc;
using SolveTracker.Models.Common;
using SolveTracker.Utilities.Attributes;

namespace SolveTracker.Controllers
{
    public class ProgrammersController : Controller
    {
        [RoleAuthorize(UserRole.Teacher)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
