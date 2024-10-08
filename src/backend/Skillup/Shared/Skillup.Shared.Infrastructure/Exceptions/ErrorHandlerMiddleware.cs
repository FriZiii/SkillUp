using Microsoft.AspNetCore.Http;
using System.Net;

namespace Skillup.Shared.Infrastructure.Exceptions
{
    internal sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionCompositionRoot _exceptionCompositionRoot;

        public ErrorHandlerMiddleware(IExceptionCompositionRoot exceptionCompositionRoot)
        {
            _exceptionCompositionRoot = exceptionCompositionRoot;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                // TODO : LOGS
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
