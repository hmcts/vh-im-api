using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InstantMessagingAPI.Common;
using InstantMessagingAPI.DAL.Exceptions;

namespace InstantMessagingAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) when (ex is InstantMessagingDalException || ex is BadRequestException)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(exception.Message);
        }
    }
}
