using Microsoft.AspNetCore.Mvc;
using SolveTracker.Domain.Entities.Common;
using SolveTracker.Web.Extensions.Attributes;

namespace SolveTracker.Web.Controllers;

public class ProgrammersController : Controller
{
    [RoleAuthorize(UserRole.Teacher)]
    public IActionResult Index()
    {
        return View();
    }
}
