using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces.Middleware;

public interface ITokenBlacklistMiddleware
{
    public Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider);
}