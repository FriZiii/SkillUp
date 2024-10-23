using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Shared.Infrastructure.Exceptions
{
    public interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}
