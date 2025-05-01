using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SolveTracker.Models.Login;
using SolveTracker.Services.Login;
using SolveTracker.ViewModels.Login;
using System.Security.Claims;

namespace SolveTracker.Controllers
{
    public class LoginController(ILoginService loginService, IMapper mapper) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginInfo = mapper.Map<LoginRequest>(model);
                var loginResponse = await loginService.IsLoginInformationValidAsync(loginInfo);

                if (!string.IsNullOrEmpty(loginResponse?.UserId))
                {
                    var claims = new List<Claim>
                    {
                        new (ClaimTypes.Sid, loginResponse.UserId),
                        new (ClaimTypes.Name, loginResponse.Name),
                        new (ClaimTypes.Role, loginResponse.Role.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
