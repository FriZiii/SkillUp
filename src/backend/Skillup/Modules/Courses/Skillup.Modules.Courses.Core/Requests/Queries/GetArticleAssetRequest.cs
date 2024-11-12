using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetArticleAssetRequest(Guid ElementId) : IRequest<ArticleAssetDto>;
}
