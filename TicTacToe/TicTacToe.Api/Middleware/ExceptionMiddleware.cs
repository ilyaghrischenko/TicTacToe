using System.Net;
using TicTacToe.Contracts.Middleware;

namespace TicTacToe.Api.Middleware;

public class ExceptionMiddleware : IExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (ArgumentException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.Unauthorized,
                ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.InternalServerError,
                ex.Message);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "text/plain";

        await context.Response.WriteAsync(message);
    }
}