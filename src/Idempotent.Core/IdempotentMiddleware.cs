using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Idempotent.Core
{
    public class IdempotentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IdempotentMiddlewareOptions _options;
        private readonly ILogger _logger;

        public IdempotentMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<IdempotentMiddleware>();
            _options = new IdempotentMiddlewareOptions();
        }

        public IdempotentMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IdempotentMiddlewareOptions options)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<IdempotentMiddleware>();
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context, IDistributedCache cache)
        {
            //context.Response
            await _next(context);
        }
    }

    public static class IdempotentMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdempotentRequests(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdempotentMiddleware>();
        }
    }

    public class IdempotentMiddlewareOptions
    {
        public string IdempotentKeyHttpHeader { get; set; }
    }
}