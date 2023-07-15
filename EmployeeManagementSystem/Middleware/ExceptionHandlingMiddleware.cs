using EMS.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            Error error = new Error();
            int StatusCode = 500;
            if (ex is ApiException apiException)
            {
                error.Message = (apiException).Message;
                StatusCode = (apiException).Code;
            }
            else if (ex != null)
            {
                error.Message = $"Internal Server Error";
            }
            var errorMessage = JsonConvert.SerializeObject(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCode;
            if (StatusCode == 400)
            {
                var list = new List<object>
                {
                new { propertyName = "null", customState = "Exception",errorMessage= ex.Message,errorCode="400" }
            };
                return context.Response.WriteAsync(JsonConvert.SerializeObject(list));
            }
            return context.Response.WriteAsync(errorMessage);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExceptionHandlingMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
