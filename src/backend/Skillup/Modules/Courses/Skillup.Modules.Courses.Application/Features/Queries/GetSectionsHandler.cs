using MassTransit.Initializers;
using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetSectionsHandler : IRequestHandler<GetSectionsRequest, List<SectionDto>>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionsHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<List<SectionDto>> Handle(GetSectionsRequest request, CancellationToken cancellationToken)
        {
            SectionMapper sectionMapper = new();
            var sections = (await _sectionRepository.GetSectionsByCourseId(request.CourseId))
                .Select(section => { section.Elements = section.Elements.OrderBy(e => e.Index).ToList(); return section; });

            var sectionsDtos = sections.Select(sectionMapper.SectionToSectionDto).ToList();
            return sectionsDtos;
        }
    }
}
