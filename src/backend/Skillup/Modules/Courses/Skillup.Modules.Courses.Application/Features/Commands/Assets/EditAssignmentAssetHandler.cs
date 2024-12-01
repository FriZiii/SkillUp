using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets
{
    internal class EditAssignmentAssetHandler(IAssetsRepository assetsRepository, ILogger<EditAssignmentAssetHandler> logger) : IRequestHandler<EditAssignmentAssetRequest>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;
        private readonly ILogger<EditAssignmentAssetHandler> _logger = logger;

        public async Task Handle(EditAssignmentAssetRequest request, CancellationToken cancellationToken)
        {
            Assignment assignment = (Assignment)await _assetsRepository.GetByElementId(request.ElementId);
            assignment.Instruction = request.Instruction;
            await _assetsRepository.EditAssignment(assignment);
            _logger.LogInformation("Assignment edited");
        }
    }
}
