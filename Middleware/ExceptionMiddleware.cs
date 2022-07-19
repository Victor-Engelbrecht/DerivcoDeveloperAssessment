using Microsoft.AspNetCore.Builder;
using Middleware;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extentions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ExceptionMiddlewareLogic>();
        }
    }
}
