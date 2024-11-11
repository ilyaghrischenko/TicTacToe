using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Contracts.Interfaces.Middleware;

public interface IExceptionMiddleware
{
    public Task InvokeAsync(HttpContext context);
    public Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message);
}