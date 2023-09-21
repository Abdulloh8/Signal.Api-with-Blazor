using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Pro.Helpers;


public class ErrorHanlerdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHanlerdMiddleware> _logger;

    public ErrorHanlerdMiddleware(RequestDelegate next, ILogger<ErrorHanlerdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "l_error");
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new {Error="error new"});
        }
        
    }
}


public static class ErrorHanlerdMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHanlerdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHanlerdMiddleware>();
    }
}
