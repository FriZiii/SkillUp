using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets
{
    internal class GetVideoAssetHandler(IAssetsRepository assetsRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetVideoAssetRequest, VideoAssetDto>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<VideoAssetDto> Handle(GetVideoAssetRequest request, CancellationToken cancellationToken)
        {
            var video = await _assetsRepository.GetByElementId(request.ElementId) as Video ?? throw new Exception(); // TODO: Custom ex: Video for element with id doesnt exist

            var videoUrl = await _amazonS3Service.GetPresignedUrl(S3FolderPaths.CourseAsset + video.Key); // TODO: Change timeToLive of presignedURL

            return new VideoAssetDto()
            {
                ElementId = request.ElementId,
                AssetId = video.Id,
                Url = new Uri(videoUrl)
            };
        }
    }
}
