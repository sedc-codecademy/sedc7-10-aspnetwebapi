using Facebook.WebApi2_0.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Facebook.WebApi2_0.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            //catch (UserNotFoundException ex)
            //{
            //    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //    await httpContext.Response.WriteAsync(ex.Message);
            //}
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
                //TODO: log the exception
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            if (ex is ArgumentException || ex is UserNotFoundException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(ex.Message);
                return;
            }

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
