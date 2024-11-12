using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets
{
    internal class DeleteAssetHandler(IAmazonS3Service amazonS3Service, IAssetsRepository assetsRepository, IElementRepository elementRepository) : IRequestHandler<DeleteAssetRequest>
    {
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;
        private readonly IAssetsRepository _assetsRepository = assetsRepository;
        private readonly IElementRepository _elementRepository = elementRepository;

        public async Task Handle(DeleteAssetRequest request, CancellationToken cancellationToken)
        {
            var element = await _elementRepository.GetById(request.ElementId) ?? throw new Exception();  // Element with id no exist

            var asset = await _assetsRepository.GetByElementId(request.ElementId) ?? throw new Exception();  // Asset with id no exist

            switch (element.AssetType)
            {
                case AssetType.Article:
                    if (asset is Article article)
                    {
                        await _amazonS3Service.Delete(S3FolderPaths.CourseAsset + article.Key);
                    }
                    break;

                case AssetType.Video:
                    if (asset is Video video)
                    {
                        await _amazonS3Service.Delete(S3FolderPaths.CourseAsset + video.Key);
                    }
                    break;

                case AssetType.Exercise:
                    break;

                default:
                    throw new Exception();  // Wrong asset type
            }

            await _assetsRepository.Delete(asset.Id, element.AssetType);
        }
    }
}
