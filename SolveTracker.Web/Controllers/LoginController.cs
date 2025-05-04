using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SolveTracker.Application.Services.Login;
using SolveTracker.Domain.Entities.Login;
using SolveTracker.Web.Models.Login;
using System.Security.Claims;

namespace SolveTracker.Web.Controllers;

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
            LoginRequest loginInfo = mapper.Map<LoginRequest>(model);
            LoginResponse loginResponse = await loginService.IsLoginInformationValidAsync(loginInfo);

            if (!string.IsNullOrEmpty(loginResponse?.UserId))
            {
                List<Claim> claims =
                [
                    new (ClaimTypes.Sid, loginResponse.UserId),
                    new (ClaimTypes.Name, loginResponse.Name),
                    new (ClaimTypes.Role, loginResponse.Role.ToString())
                ];

                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);
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
