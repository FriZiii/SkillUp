using System.Net;

namespace Skillup.Shared.Abstractions.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
