using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolveTracker.Application.Services.Registration;
using SolveTracker.Domain.Entities.Registration;
using SolveTracker.Web.Models.Registration;

namespace SolveTracker.Web.Controllers;

public class RegistrationController(IRegistrationService registrationService, IMapper mapper) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(RegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            RegistrationRequest request = mapper.Map<RegistrationRequest>(model);
            await registrationService.CreateAccountAsync(request);
            return RedirectToAction("Index", "Dashboard");
        }

        return View(model);
    }
}
