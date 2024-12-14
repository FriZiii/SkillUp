using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;

namespace Skillup.Modules.Courses.Core.Requests.Queries.Assets
{
    public record GetVideoAssetRequest(Guid ElementId) : IRequest<VideoAssetDto>;
}
