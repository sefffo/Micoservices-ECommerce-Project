using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.SharedLibirary.MiddleWares
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var sigendHeader = context.Request.Headers["Api-Gateway"];
            //if its outside the api gateway return null 
            if(string.IsNullOrEmpty(sigendHeader))
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable; // service unavailable
                await context.Response.WriteAsync("Unauthorized: Missing Api-Gateway header.");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
