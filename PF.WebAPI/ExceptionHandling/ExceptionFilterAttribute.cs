using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PF.WebAPI.Services.Filtering;

namespace PF.WebAPI.ExceptionHandling
{
    public class ExceptionFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger<ExceptionFilterAttribute> _logger;

        public ExceptionFilterAttribute(ILogger<ExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext?.Response == null)
                return;

            if (context.Exception is NumberExceedsPrimeSearchBoundsException e)
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.WriteAsync($"{e.Number} was too large for the applied primality test algorithm's limit: {e.Limit}", new CancellationToken()).Wait();
            }
            else
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = 500;
                context.HttpContext.Response.WriteAsync("An exception has been thrown. See logs for details", new CancellationToken()).Wait();
                _logger.LogError(context.Exception, "");
            }

            context.ExceptionHandled = true;
        }
    }
}
