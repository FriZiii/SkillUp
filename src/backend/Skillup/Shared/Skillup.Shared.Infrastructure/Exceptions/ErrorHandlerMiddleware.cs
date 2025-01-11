using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Skillup.Shared.Infrastructure.Exceptions
{
    internal sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionCompositionRoot _exceptionCompositionRoot;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(IExceptionCompositionRoot exceptionCompositionRoot, ILogger<ErrorHandlerMiddleware> logger)
        {
            _exceptionCompositionRoot = exceptionCompositionRoot;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var exceptionType = exception.GetType().FullName;
                var stackTrace = exception.StackTrace;

                var stackFrames = new System.Diagnostics.StackTrace(exception, true).GetFrames();
                if (stackFrames != null && stackFrames.Length > 0)
                {
                    var stackTraceInfo = new List<string>();

                    foreach (var frame in stackFrames)
                    {
                        var declaringType = frame?.GetMethod()?.DeclaringType;
                        var methodName = frame?.GetMethod()?.Name;
                        stackTraceInfo.Add($"{declaringType}.{methodName}");
                    }

                    var stackTraceString = string.Join(" -> ", stackTraceInfo);

                    _logger.LogError($"Unhandled Exception of type {exceptionType} in {stackTraceString}", exceptionType, stackTrace);
                }
                else
                {
                    _logger.LogError($"{exceptionType}: {exception.Message}", exceptionType, stackTrace);
                }

                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = _exceptionCompositionRoot.Map(exception);
            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
            var response = errorResponse?.Response;
            if (response is null)
            {
                return;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
