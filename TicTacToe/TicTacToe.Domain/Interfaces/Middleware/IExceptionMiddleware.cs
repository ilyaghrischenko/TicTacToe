using System.Net;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces.Middleware;

public interface IExceptionMiddleware
{
    public Task InvokeAsync(HttpContext context);
    public Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message);
}