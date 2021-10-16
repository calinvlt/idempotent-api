using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Idempotent.Core
{
    public class IdempotentMiddleware
    {
        private readonly RequestDelegate _next;

        public IdempotentMiddleware(IDistributedCache cache)
        {
            if (cache == null) throw new Exception("Cache cannot be null");
        }

        public IdempotentMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task InvokeAsync(HttpContext context, IDistributedCache cache)
        {
            if (cache == null) throw new Exception("Cache cannot be null");
            
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