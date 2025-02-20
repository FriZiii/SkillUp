﻿using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets
{
    internal class GetAssignmentAssetHandler(IAssetsRepository assetsRepository) : IRequestHandler<GetAssignmentAssetRequest, AssignmentAssetDto>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;

        public async Task<AssignmentAssetDto> Handle(GetAssignmentAssetRequest request, CancellationToken cancellationToken)
        {
            var assignment = await _assetsRepository.GetByElementId(request.ElementId) as Assignment ?? throw new NotFoundException($"Assignment for Element with ID {request.ElementId} not found");
            var mapper = new AssignmentMapper();

            return mapper.AssignmentToAssignmentDto(assignment);
        }
    }
}
