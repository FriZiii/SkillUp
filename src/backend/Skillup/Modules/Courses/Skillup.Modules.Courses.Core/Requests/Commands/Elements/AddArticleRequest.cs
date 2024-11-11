using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record AddArticleRequest(
        string Title,
        string Description,
        AssetType Type,
        int Index,
        bool IsFree,
        Guid SectionId,
        string HTMLContent) : IRequest;
}
