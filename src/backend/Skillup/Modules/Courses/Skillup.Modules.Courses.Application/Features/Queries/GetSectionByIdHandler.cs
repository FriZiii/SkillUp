using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetSectionByIdHandler : IRequestHandler<GetSectionByIdRequest, SectionDto>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionByIdHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task<SectionDto> Handle(GetSectionByIdRequest request, CancellationToken cancellationToken)
        {
            SectionMapper sectionMapper = new();
            var section = await _sectionRepository.GetById(request.SectionId);
            var sectionDto = sectionMapper.SectionToSectionDto(section);
            return sectionDto;
        }
    }
}
