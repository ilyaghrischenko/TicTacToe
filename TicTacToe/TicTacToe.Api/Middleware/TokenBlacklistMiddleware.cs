using TicTacToe.Domain.Interfaces.Middleware;
using TicTacToe.Domain.Interfaces.TokenServices;

namespace TicTacToe.Api.Middleware;

public class TokenBlacklistMiddleware : ITokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;

    public TokenBlacklistMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var blacklistService = serviceProvider.GetRequiredService<ITokenBlacklistService>();

            if (await blacklistService.IsTokenBlacklistedAsync(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token is inactive.");
                return;
            }
        }

        await _next(context);
    }
}
