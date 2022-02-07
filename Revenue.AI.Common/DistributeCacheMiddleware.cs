using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenue.AI.Middleware.Common
{
    //public class DistributeCacheMiddleware : IMiddleware
    //{
    //    private readonly IRedisDestributeCache redisDestributeCache;

    //    public DistributeCacheMiddleware(IRedisDestributeCache cache)
    //    {
    //        redisDestributeCache = cache;
    //    }

    //    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    //    {
    //        var isRequestMethodGet = context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase);
    //        //string authToken = "";
    //        //var path = context.Request.Path.ToString() + context.Request.QueryString.ToString();
    //        //if (context.Request.Headers.ContainsKey("Authorization"))
    //        //{
    //        //    authToken = context.Request.Headers["Authorization"];
    //        //}
            

    //        string itemCache = null;
    //        if (isRequestMethodGet)
    //        {
    //            if (redisDestributeCache.TryGetString("cachedTimeUTC", out string cacheItem))
    //            {

    //                itemCache = cacheItem;
    //                if (!context.Response.HasStarted)
    //                {
    //                    context.Response.Headers.Clear();
    //                    await context.Response.WriteAsync(cacheItem);
    //                }
    //            }
    //            else
    //            {
    //                //await redisDestributeCache.SetStringAsync("cachedTimeUTC", tenantId);
    //            }
    //        }
    //        if (itemCache == null)
    //        {

    //            await next(context);
    //        }
    //    }

    //    private Task WriteResponseAsync(HttpContext context, string response = "")
    //    {
    //        return context.Response.WriteAsync(response);
    //    }
    //}
}
