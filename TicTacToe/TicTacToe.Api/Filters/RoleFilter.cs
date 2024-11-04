using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Api.Filters;

public class RoleFilter : IAuthorizationFilter
{
    private readonly Role _requiredRole;

    public RoleFilter(Role requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userClaims = context.HttpContext.User;
        var roleClaim = userClaims.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(roleClaim))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (roleClaim == Role.Blocked.ToString())
        {
            context.Result = new ForbidResult();
        }
        else if (roleClaim == Role.User.ToString() && _requiredRole == Role.Admin)
        {
            context.Result = new ForbidResult();
        }
        else if (roleClaim == Role.Admin.ToString() && _requiredRole == Role.User)
        {
            context.Result = new ForbidResult();
        }
    }
}