
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record EditCourseTumbnailRequest(Guid CourseId, IFormFile File) : IRequest;
}
