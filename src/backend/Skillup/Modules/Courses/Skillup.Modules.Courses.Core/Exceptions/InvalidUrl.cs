using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Courses.Core.Exceptions
{
    public class InvalidUrl : SkillupException
    {
        public InvalidUrl() : base("Invalid url address.")
        {
        }
    }
}
