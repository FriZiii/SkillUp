namespace Skillup.Shared.Abstractions.Exceptions.GlobalExceptions
{
    public class BadRequestException(string message) : Exception(message)
    {
    }
}
