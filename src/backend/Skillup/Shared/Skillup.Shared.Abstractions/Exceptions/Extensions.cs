using Microsoft.Extensions.Logging;

namespace Skillup.Shared.Abstractions.Exceptions
{
    public static class Extensions
    {
        public static ILogger LogExceptions(this ILogger logger, Exception exception)
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
                    var lineNumber = frame?.GetFileLineNumber();
                    stackTraceInfo.Add($"{declaringType?.FullName}.{methodName} (Line {lineNumber})");
                }

                var stackTraceString = string.Join(" -> ", stackTraceInfo);
                logger.LogError(
                    exception,
                    $"Unhandled Exception of type {exceptionType} occurred in method chain: {stackTraceString}. Exception message: {exception.Message}");
            }
            else
            {
                logger.LogError(
                    exception,
                    $"{exceptionType}: {exception.Message}. StackTrace: {stackTrace ?? "No stack trace available"}");
            }

            return logger;
        }
    }
}