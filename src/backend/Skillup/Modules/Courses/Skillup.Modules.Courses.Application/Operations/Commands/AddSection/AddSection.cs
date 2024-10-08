using MediatR;

namespace Skillup.Modules.Courses.Application.Operations.Commands.AddSection
{
    public record AddSection : IRequest
    {
        public string Title { get; set; }
        public Guid CourseId { get; set; }
    }
}
