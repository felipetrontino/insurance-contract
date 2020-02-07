using Insurance.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Insurance.Api.Middlewares
{
    public class GlobalErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var httpStatusCode = ConfigurateExceptionTypes(ex);
                httpContext.Response.StatusCode = httpStatusCode;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(
                    JsonConvert.SerializeObject(new
                    {
                        message = ex.Message
                    }));
            }
        }

        private static int ConfigurateExceptionTypes(Exception exception)
        {
            return exception switch
            {
                var _ when exception is ValidationBusinessException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}