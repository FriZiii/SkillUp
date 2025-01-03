using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetCompletionCertificateRequest(Guid CourseId, Guid UserId) : IRequest<FileDto>;
}
