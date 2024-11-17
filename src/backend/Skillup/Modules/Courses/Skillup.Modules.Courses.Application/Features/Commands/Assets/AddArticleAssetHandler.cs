using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets
{
    internal class AddArticleAssetHandler(IAmazonS3Service amazonS3service, IAssetsRepository assetsRepository) : IRequestHandler<AddArticleAssetRequest>
    {
        private readonly IAmazonS3Service _amazonS3Service = amazonS3service;
        private readonly IAssetsRepository _assetsRepository = assetsRepository;

        public async Task Handle(AddArticleAssetRequest request, CancellationToken cancellationToken)
        {
            await _amazonS3Service.Upload(request.File, S3FolderPaths.CourseAsset + request.Key);
            await _assetsRepository.AddArticle(new Article()
            {
                ElementId = request.ElementId,
                Key = request.Key,
            });
        }
    }
}
