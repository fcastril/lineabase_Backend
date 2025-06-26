using Api.Generator.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Api.MiddleException
{
    public class MiddleHandlerException
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MiddleHandlerException> _logger;

        public MiddleHandlerException(RequestDelegate next, ILogger<MiddleHandlerException> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionResponseApi(context, ex);
            }
        }

        protected Task ExceptionResponseApi(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ResponseApi<object>
            {
                Status = false,
                Data = $"{exception?.Message} --inner-- {exception?.InnerException?.ToString() ?? string.Empty}",
                Message = "We present a technical failure, it is not possible to process the request."
            };
            if (exception?.GetType() == typeof(Exception))
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var ex = exception;
                response = new ResponseApi<object> { Status = false, Data = new object(), Message = ex.Message };
            }

            if (exception?.GetType() == typeof(BadRequest))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response = new ResponseApi<object> { Status = false, Data = new object(), Message = exception.Message };
            }

            if (exception?.GetType() == typeof(UnauthorizedAccessException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var ex = (UnauthorizedAccessException)exception;
                response = new ResponseApi<object> { Status = false, Data = "You are not authorized to perform this action.", Message = ex.Message  };
            }
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
