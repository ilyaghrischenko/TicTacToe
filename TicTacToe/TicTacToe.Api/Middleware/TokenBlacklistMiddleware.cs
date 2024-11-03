using TicTacToe.Domain.Interfaces.TokenServices;

namespace TicTacToe.Api.Middleware;

public class TokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenBlacklistService _blacklistService;

    public TokenBlacklistMiddleware(RequestDelegate next, ITokenBlacklistService blacklistService)
    {
        _next = next;
        _blacklistService = blacklistService;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null && await _blacklistService.IsTokenBlacklistedAsync(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token is inactive.");
            return;
        }

        await _next(context);
    }
}
