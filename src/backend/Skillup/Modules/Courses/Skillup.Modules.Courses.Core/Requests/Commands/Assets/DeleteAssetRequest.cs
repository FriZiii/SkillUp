using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record DeleteAssetRequest(Guid AssetId, AssetType AssetType) : IRequest;
}
