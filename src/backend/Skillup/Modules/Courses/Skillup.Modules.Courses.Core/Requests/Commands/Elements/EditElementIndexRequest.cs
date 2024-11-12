using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record EditElementIndexRequest(Guid ElementId, int index) : IRequest<SectionDto>;
}
