using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Api.Filters;

public class RoleAttribute : TypeFilterAttribute
{
    public RoleAttribute(Role requiredRole) : base(typeof(RoleFilter))
    {
        Arguments = new object[] { requiredRole };
    }
}