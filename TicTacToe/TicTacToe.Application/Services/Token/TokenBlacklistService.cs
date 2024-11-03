using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Domain.Interfaces.TokenServices;

namespace TicTacToe.Application.Services.Token;

public class TokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;

    public TokenBlacklistMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var tokenBlacklistService = scope.ServiceProvider.GetRequiredService<ITokenBlacklistService>();

            // Implement your token blacklisting logic here

            await _next(context);
        }
    }
}