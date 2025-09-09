using E_Commerce.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Api.Middleware
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache memoryCache;
        private TimeSpan _ratelimitwindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            this.memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _applySecurity(context);

                if (_isReQuestallowed(context) == false)
                {
                    context.Response.StatusCode =(int) HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ExceptionApi((int)HttpStatusCode.TooManyRequests, "Too Many Request Try Anther Time" );
                    await context.Response.WriteAsJsonAsync(response);
                }
                await _next(context);
            }
            catch (Exception Ex)
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _environment.IsDevelopment() ? new ExceptionApi((int)HttpStatusCode.InternalServerError, Ex.Message, Ex.StackTrace)
                    : new ExceptionApi((int)HttpStatusCode.InternalServerError, Ex.Message);

                var Json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(Json);


            }
        
        }
        private bool _isReQuestallowed(HttpContext context)
        { 
           var Ip = context.Connection.RemoteIpAddress.ToString();
            var Cashkey = $" Rate : {Ip}";
            var DateNow = DateTime.Now;

            var (TimesTamp, Count) = memoryCache.GetOrCreate(Cashkey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _ratelimitwindow;
                return (TimesTamp : DateNow,Count : 0);
            });

            if (DateNow - TimesTamp < _ratelimitwindow)
            {
                if (Count >= 8)
                {
                    return false;
                }
                memoryCache.Set(Cashkey, (TimesTamp, Count + 1), _ratelimitwindow);
            }
            else 
            {
                memoryCache.Set(Cashkey, (TimesTamp, Count ), _ratelimitwindow);

            }
            return true;
           
        
        
        }

        private void _applySecurity(HttpContext context)
        {
            context.Response.Headers["X-Frame-Options"] = "DENY";

            context.Response.Headers["X-XSS-Protection"] = "1; mode=block";

            context.Response.Headers["X-Content-Type-Options"] = "nosniff";

            context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";

            context.Response.Headers["Content-Security-Policy"] = "default-src 'self'";
        }
    }
}
