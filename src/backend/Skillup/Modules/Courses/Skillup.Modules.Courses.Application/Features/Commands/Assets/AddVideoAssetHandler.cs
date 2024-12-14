using MediatR;
using Microsoft.AspNetCore.Http;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets
{
    internal class AddVideoAssetHandler(IAmazonS3Service amazonS3service, IAssetsRepository assetsRepository) : IRequestHandler<AddVideoAssetRequest>
    {
        private readonly IAmazonS3Service _amazonS3Service = amazonS3service;
        private readonly IAssetsRepository _assetsRepository = assetsRepository;

        private static readonly string[] AllowedVideoMimeTypes = new[]
        {
            "video/mp4",
            "video/mkv",
        };

        public async Task Handle(AddVideoAssetRequest request, CancellationToken cancellationToken)
        {
            if (request.File == null)
            {
                throw new ArgumentException("No file provided"); //TODO: Custom ex: No video file provided
            }

            if (!IsVideoFile(request.File))
            {
                throw new ArgumentException("Provided file is not a valid video format"); //TODO: Custom ex: Provided file is not a valid video format
            }

            await _amazonS3Service.Upload(request.File, S3FolderPaths.CourseAsset + request.Key);
            await _assetsRepository.AddVideo(new Video()
            {
                ElementId = request.ElementId,
                Key = request.Key,
            });
        }

        private bool IsVideoFile(IFormFile file) => AllowedVideoMimeTypes.Contains(file.ContentType.ToLower());
    }
}
