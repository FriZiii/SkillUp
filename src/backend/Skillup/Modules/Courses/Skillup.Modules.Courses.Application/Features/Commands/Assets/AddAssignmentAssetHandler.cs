﻿using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets
{
    internal class AddAssignmentAssetHandler(IAssetsRepository assetsRepository, ILogger<AddAssignmentAssetHandler> logger) : IRequestHandler<AddAssignmentAssetRequest, AssignmentAssetDto>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;
        private readonly ILogger<AddAssignmentAssetHandler> _logger = logger;

        public async Task<AssignmentAssetDto> Handle(AddAssignmentAssetRequest request, CancellationToken cancellationToken)
        {
            var assignment = new Assignment()
            {
                ElementId = request.ElementId,
                ExerciseType = request.ExerciseType,
                Instruction = request.Instruction,
            };
            await _assetsRepository.AddAssignment(assignment);

            var mapper = new AssignmentMapper();
            _logger.LogInformation("Assignment created");
            return mapper.AssignmentToAssignmentDto(assignment);
        }
    }
}
