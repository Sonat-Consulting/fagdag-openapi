using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetCoreApi.Exceptions;

namespace NetCoreApi.Middleware
{
    /// <summary>
    ///     Mapper om exceptions til response statuser
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException _:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;//404
                        break;
                    case MappingException _:
                        context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;//422
                        break;
                    case BadRequestException _:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;//400
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500
                        break;
                }

                context.Response.Headers.Clear();
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}