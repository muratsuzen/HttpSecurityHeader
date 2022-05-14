using System.Net;
using httpsecurityheader;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAntiforgery(x =>
{
    x.SuppressXFrameOptionsHeader = true;
});

builder.Services.AddHsts(x =>
{
    x.Preload = true;
    x.IncludeSubDomains = true;
    x.MaxAge = TimeSpan.FromDays(60);
    x.ExcludedHosts.Add("example.com");
    x.ExcludedHosts.Add("www.example.com");
});

builder.Services.AddHttpsRedirection(x =>
{
    x.HttpsPort = 7047;
    x.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
});


var app = builder.Build();

app.UseMiddleware<CustomSecurityHeader>();

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("X-Frame-Options","DENY");
//    await next();
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies","none");
//    await next();
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
//    await next();
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
//    await next();
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
//    await next();
//});

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Permissions-Policy", "camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), usb=()");
//    await next();
//});

//app.Use(async (ctx, next) =>
//{
//    ctx.Response.Headers.Add("Content-Security-Policy",
//        "default-src 'self'");
//    await next();
//});



app.UseHttpsRedirection();
app.UseHsts();

app.MapGet("/", () => "Hello World!");

app.Run();
