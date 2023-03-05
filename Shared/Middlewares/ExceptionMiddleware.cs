using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Dtos;

namespace Shared.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var bodyAsText = string.Empty;
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "{@protocol} {@scheme} {@method} {@path} {@queryString} {@body}",
                    httpContext.Request.Protocol,
                    httpContext.Request.Scheme,
                    httpContext.Request.Method,
                    httpContext.Request.Path.Value,
                    httpContext.Request.QueryString.Value,
                    bodyAsText);

            await HandleExceptionAsync(httpContext);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsJsonAsync(new MessageResponse
        {
            Msg = "خطأ غير متوقع بالخادم، يرجى الاتصال بالدعم الفني"
        });
    }
}