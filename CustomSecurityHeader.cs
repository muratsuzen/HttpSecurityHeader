namespace httpsecurityheader;

public class CustomSecurityHeader
{
    private readonly RequestDelegate _next;

    public CustomSecurityHeader(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("X-Frame-Options", "DENY");

        context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

        context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");

        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

        context.Response.Headers.Add("Referrer-Policy", "no-referrer");

        context.Response.Headers.Add("Permissions-Policy", "camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), usb=()");

        context.Response.Headers.Add("Content-Security-Policy",
            "default-src 'self'");

        await _next.Invoke(context);
    }
}