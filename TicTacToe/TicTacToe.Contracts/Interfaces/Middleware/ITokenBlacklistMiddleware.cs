using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Contracts.Interfaces.Middleware;

public interface ITokenBlacklistMiddleware
{
    public Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider);
}