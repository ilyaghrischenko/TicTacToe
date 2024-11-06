using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces.Middleware;

public interface ITokenBlacklistMiddleware
{
    public Task Invoke(HttpContext context, IServiceProvider serviceProvider);
}