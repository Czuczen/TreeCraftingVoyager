using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace TreeCraftingVoyager.Server.Attributes.Filter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        private readonly int _maxRequests;
        private readonly TimeSpan _timeSpan;

        public RateLimitAttribute(int maxRequests, int seconds)
        {
            _maxRequests = maxRequests;
            _timeSpan = TimeSpan.FromSeconds(seconds);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var memoryCache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            var key = GenerateClientKey(context.HttpContext);

            if (memoryCache.TryGetValue(key, out int requestCount))
            {
                if (requestCount >= _maxRequests)
                {
                    context.Result = new ContentResult
                    {
                        Content = "Requests limit exceeded. Try again later.",
                        StatusCode = StatusCodes.Status429TooManyRequests
                    };

                    return;
                }
            }

            memoryCache.Set(key, requestCount + 1, _timeSpan);
            base.OnActionExecuting(context);
        }

        private string GenerateClientKey(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress.ToString();
            var path = context.Request.Path.ToString();
            return $"{clientIp}:{path}";
        }
    }
}
