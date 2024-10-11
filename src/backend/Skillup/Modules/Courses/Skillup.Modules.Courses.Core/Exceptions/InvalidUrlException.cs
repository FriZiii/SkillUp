using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Courses.Core.Exceptions
{
    public class InvalidUrlException : SkillupException
    {
        public InvalidUrlException() : base("Invalid url address.")
        {
        }
    }
}
