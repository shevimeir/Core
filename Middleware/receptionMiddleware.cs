namespace Core.Middleware;

public class receptionMiddleware
{
    private readonly RequestDelegate next;

    public receptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    // IConfiguration is injected per-request, so flipping the flag in appsettings
    // takes effect on the next request without restarting the app.
    public async Task Invoke(HttpContext context, IConfiguration config)
    {
        if (config.GetValue<bool>("receptionClosed"))
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable; // 503
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("The reception is closed. Please come back later.");
            return; // short-circuit: next is NEVER called
        }
      

        await next.Invoke(context);
    }
}

public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UseReceptionClosed(this IApplicationBuilder builder)
        => builder.UseMiddleware<receptionMiddleware>();
}
