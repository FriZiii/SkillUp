using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets
{
    internal class AddAssignmentAssetHandler(IAssetsRepository assetsRepository) : IRequestHandler<AddAssignmentAssetRequest, AssignmentDto>
    {
        private readonly IAssetsRepository _assetsRepository = assetsRepository;

        public async Task<AssignmentDto> Handle(AddAssignmentAssetRequest request, CancellationToken cancellationToken)
        {
            var assignment = new Assignment()
            {
                ElementId = request.ElementId,
                Instruction = request.Instruction,
            };
            await _assetsRepository.AddAssignment(assignment);

            var mapper = new AssignmentMapper();
            return mapper.AssignmentToAssignmentDto(assignment);
        }
    }
}
