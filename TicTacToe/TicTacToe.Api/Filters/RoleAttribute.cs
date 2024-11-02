using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Api.Filters;

public class RoleAttribute : TypeFilterAttribute
{
    public RoleAttribute(string role) : base(typeof(RoleFilter))
    {
        Arguments = new object[] { role };
    }
}