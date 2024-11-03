using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicTacToe.Api.Filters;

public class RoleFilter : IAuthorizationFilter
{
    private readonly string _role;
    
    public RoleFilter(string role)
    {
        _role = role;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new RedirectResult("/pages/auth.html");
            return;
        }
        
        var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (role == "Banned")
        {
            context.Result = new RedirectResult("/pages/banned.html");
        }
    }
}