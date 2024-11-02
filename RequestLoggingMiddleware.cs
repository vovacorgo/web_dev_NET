using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

namespace SupportCenter
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var url = context.Request.GetDisplayUrl();
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var requestTimeUtc = DateTime.UtcNow;
            var requestTime = requestTimeUtc.ToLocalTime(); 

            Log.Information("Request - URL: {Url}, Time: {Time}, IP Address: {IP}", url, requestTime, ip);

            await _next(context);

        }
    }
}
