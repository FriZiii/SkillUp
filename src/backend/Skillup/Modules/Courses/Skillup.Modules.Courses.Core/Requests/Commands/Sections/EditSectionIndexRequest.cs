
using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Sections
{
    public record EditSectionIndexRequest(Guid SectionId, int index) : IRequest<List<SectionDto>>;
}
