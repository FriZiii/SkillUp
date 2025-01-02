namespace Skillup.Shared.Abstractions.Exceptions.GlobalExceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
