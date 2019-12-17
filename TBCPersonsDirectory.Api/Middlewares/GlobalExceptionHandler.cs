using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBCPersonsDirectory.Api.Middlewares
{
    public static class GlobalExceptionHandler
    {
        public static void UseGlobalExceptionHandling(this IApplicationBuilder app) {

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = 500;
                    if (exceptionHandlerFeature != null)
                    {
                        var innerExceptionMessage = exceptionHandlerFeature.Error.InnerException == null ? "" : exceptionHandlerFeature.Error.InnerException.Message;
                        Log.Error($"Error: {exceptionHandlerFeature.Error}, Message: {exceptionHandlerFeature.Error.Message}," +
                            $" InnerException: {exceptionHandlerFeature.Error.InnerException}, InnerExceptionMessage: {innerExceptionMessage}");
                    }
                });
            });
        }
    }
}
