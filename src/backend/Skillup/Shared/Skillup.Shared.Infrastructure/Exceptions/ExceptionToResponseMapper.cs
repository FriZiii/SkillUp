using Skillup.Shared.Abstractions.Exceptions;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using System.Collections.Concurrent;
using System.Net;

namespace Skillup.Shared.Infrastructure.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                UnauthorizedException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                    , HttpStatusCode.Unauthorized),

                NotFoundException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                    , HttpStatusCode.NotFound),

                BadRequestException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                    , HttpStatusCode.BadRequest),

                _ => new ExceptionResponse(new ErrorsResponse(new Error("Error", "Server error")),
                    HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);

        private record ErrorsResponse(params Error[] Errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, type.Name);
        }
    }
}
