using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SolveTracker.Domain.Entities.Common;

namespace SolveTracker.Web.Extensions.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class RoleAuthorizeAttribute(params UserRole[] roles) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(roles);

        var user = context.HttpContext.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
        else if (roles.FirstOrDefault() != UserRole.All && !roles.Any(role => user.IsInRole(role.ToString())))
        {
            context.Result = new RedirectToActionResult("AccessDenied", "Common", null);
        }
    }
}
