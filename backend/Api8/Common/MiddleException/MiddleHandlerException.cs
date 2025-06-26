using Util.Ex;
using Utilidades;
using Util.Common;
using Domain.Common;
using BlobStorageMtow;
using Newtonsoft.Json;
using System.Globalization;

namespace Api.Common.MiddleException {
  /// <summary>
  /// 
  /// </summary>
  public class MiddleHandlerException : Exception {
    private readonly RequestDelegate _next;
    private readonly ITableStorage<Transactions> _tableStorage;
    protected readonly IUtil _util;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    /// <param name="tableStorage"></param>
    /// <param name="util"></param>
    public MiddleHandlerException(RequestDelegate next, ITableStorage<Transactions> tableStorage, IUtil util) {
      _tableStorage = tableStorage;
      _next = next;
      _util = util;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context) {
      try {
        await _next(context);
      }
      catch (Exception ex) {
        await ExceptionResponseApi(context, ex);
      }
    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="statusCodes"></param>
        /// <returns></returns>
        protected async Task ExceptionResponseApi(HttpContext httpContext, Exception exception, int statusCodes = StatusCodes.Status500InternalServerError) {
      if (httpContext is null) {
        await CreateLog("ExceptionResponseApi is null", "httpContext");
        return;
      }

      httpContext.Response.ContentType = Constants.ContentType;
      httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
      var response = new ResponseApi<object> {
        Status = false,
        Data = $"{exception?.Message} {exception?.InnerException?.ToString() ?? string.Empty}",
        Message = Constants.MessageFail
      };

      ResponseApi<object> objectResponse;

      objectResponse = ValidateDomainException(httpContext, exception, response);
      await CreateLog(httpContext.Request.Path, objectResponse);

      await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(objectResponse).ToLower(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <param name="ObjectResponse"></param>
    /// <returns></returns>
    private ResponseApi<object> ValidateDomainException(HttpContext httpContext, Exception exception, ResponseApi<object> ObjectResponse) {
      ResponseApi<object> objectResponse = ObjectResponse;

      if (exception?.GetType() == typeof(DomainExceptionOk)) {
        httpContext.Response.StatusCode = StatusCodes.Status200OK;
        var ex = (DomainExceptionOk)exception;
        objectResponse = new ResponseApi<object> { Status = false, Data = ex.Message, Message = ex.Message };
      }

      if (exception?.GetType() == typeof(DomainException)) {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var ex = (DomainException)exception;
        objectResponse = new ResponseApi<object> { Status = false, Data = ex.Message, Message = ex.Message };
      }

      if (exception?.GetType() == typeof(DomainModelExceptions)) {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        objectResponse = new ResponseApi<object> { Status = false, Data = exception.Message, Message = exception.Message };
      }

      if (exception?.GetType() == typeof(UnauthorizedAccessException)) {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        var ex = (UnauthorizedAccessException)exception;
        objectResponse = new ResponseApi<object> { Status = false, Data = ex.Message, Message = Constants.MessageUnauthorized };
      }

      return objectResponse;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="originCategory"></param>
    /// <returns></returns>
    public async Task CreateLog(object input, object output, string originCategory = "OriginIERRORORCH") {
      await _tableStorage.InsertData(new Transactions() {
        Input = JsonConvert.SerializeObject(input),
        Output = JsonConvert.SerializeObject(output),
        subdomain = Esubdomain.IERRORORCH.ToString(),
        OriginCategory = originCategory,
        ClientId = _util.GetHeaderRequest(EHeaders.CodeClient),
        PartitionKey = Esubdomain.IERRORORCH.ToString()
      });
    }
  }
}
