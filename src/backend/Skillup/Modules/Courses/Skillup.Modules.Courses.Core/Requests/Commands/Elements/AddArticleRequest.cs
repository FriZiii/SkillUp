using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record AddArticleRequest(string Title, bool IsFree, bool IsPublished, Guid SectionId, string HTMLContent) : IRequest;
}
