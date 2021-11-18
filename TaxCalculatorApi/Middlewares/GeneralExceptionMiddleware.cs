using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TaxCalculatorApi.Core.Exceptions;

namespace TaxCalculatorApi.Middlewares
{
    public class GeneralExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleException(context.Response, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleException(context.Response, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private Task HandleException(HttpResponse response, Exception exception, HttpStatusCode statusCode, object responseToWrite = null)
        {
            response.StatusCode = (int)statusCode;

            return responseToWrite != null ?
                response.WriteAsync(JsonConvert.SerializeObject(responseToWrite))
                : Task.CompletedTask;
        }
    }
}