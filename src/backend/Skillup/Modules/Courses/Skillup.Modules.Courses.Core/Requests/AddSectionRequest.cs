using MediatR;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public record AddSectionRequest : IRequest
    {
        public string Title { get; set; }
        public Guid CourseId { get; set; }
    }
}
