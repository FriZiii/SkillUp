﻿using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets
{
    internal class GetArticleAssetHandler(IAssetsRepository assetsRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetArticleAssetRequest, ArticleAssetDto>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<ArticleAssetDto> Handle(GetArticleAssetRequest request, CancellationToken cancellationToken)
        {
            var article = await _assetsRepository.GetByElementId(request.ElementId) as Article ?? throw new NotFoundException($"Article for Element with ID {request.ElementId} not found");

            var articleUrl = await _amazonS3Service.GetPresignedUrl(S3FolderPaths.CourseAsset + article.Key);

            return new ArticleAssetDto()
            {
                ElementId = request.ElementId,
                AssetId = article.Id,
                Url = new Uri(articleUrl)
            };
        }
    }
}
