using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record AddArticleRequest(
        string Title,
        string Description,
        ElementType Type,
        int Index,
        bool IsFree,
        bool IsPublished,
        Guid SectionId,
        string HTMLContent) : IRequest;
}
