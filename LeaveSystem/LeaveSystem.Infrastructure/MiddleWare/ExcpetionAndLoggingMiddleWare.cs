using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LeaveSystem.Appliction.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LeaveSystem.Infrastructure.MiddleWare
{
    public class ExcpetionAndLoggingMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExcpetionAndLoggingMiddleWare> _logger;
        public ExcpetionAndLoggingMiddleWare(RequestDelegate requestDelegate, ILogger<ExcpetionAndLoggingMiddleWare> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
               await HandleException(context,ex);
            }

        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            string message;
            if(exception is BusinessException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = exception.Message;
            }
            context.Response.StatusCode = statusCode;
            var reponse = new
            {
                StatusCode = statusCode,
                Message = message
            };
            var json=JsonSerializer.Serialize(reponse);
            return context.Response.WriteAsync(json);
        }
    }
}
