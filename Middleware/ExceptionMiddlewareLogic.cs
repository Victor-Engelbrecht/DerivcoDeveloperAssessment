using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using System.Net;
using System.Text.Json;

namespace Middleware
{
    public class ExceptionMiddlewareLogic
    {
		private readonly RequestDelegate _next;
		public ExceptionMiddlewareLogic(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddlewareLogic> logger)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex, logger);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddlewareLogic> logger)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;
			if (exception is ErrorException errorException)
				context.Response.StatusCode = (Int32)errorException.StatusCode;
			logger.LogError(JsonSerializer.Serialize(new ErrorResponse(exception)));
			if (exception.Message.Contains("See the inner exception for details")) exception = new Exception(exception.InnerException.Message);
			var errorText = JsonSerializer.Serialize(new ErrorResponse(exception));

			return context.Response.WriteAsync(errorText);
		}
	}
}