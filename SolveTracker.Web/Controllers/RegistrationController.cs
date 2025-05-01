using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolveTracker.Models.Registration;
using SolveTracker.Services.Registration;
using SolveTracker.ViewModels.Registration;

namespace SolveTracker.Controllers
{
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
                var request = mapper.Map<RegistrationRequest>(model);
                await registrationService.CreateAccountAsync(request);
                return RedirectToAction("Index", "Dashboard");
            }

            return View(model);
        }
    }
}
