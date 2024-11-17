using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets
{
    internal class GetArticleAssetHandler(IAssetsRepository assetsRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetArticleAssetRequest, ArticleAssetDto>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<ArticleAssetDto> Handle(GetArticleAssetRequest request, CancellationToken cancellationToken)
        {
            var article = await _assetsRepository.GetByElementId(request.ElementId) as Article ?? throw new Exception(); // TODO: custom ex

            var articleUrl = await _amazonS3Service.GetPresignedUrl(S3FolderPaths.CourseAsset + article.Key); // TODO: Change timeToLive of presignedURL

            return new ArticleAssetDto()
            {
                ElementId = request.ElementId,
                AssetId = article.Id,
                Url = new Uri(articleUrl)
            };
        }
    }
}
