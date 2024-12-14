using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetProgressForCourseHandler(ICourseUserProgressRepository courseUserProgressRepository) : IRequestHandler<GetProgressForCourseRequest, IEnumerable<Guid>>
    {
        private readonly ICourseUserProgressRepository _courseUserProgressRepository = courseUserProgressRepository;

        public async Task<IEnumerable<Guid>> Handle(GetProgressForCourseRequest request, CancellationToken cancellationToken)
        {
            var progress = await _courseUserProgressRepository.GetByCourseAndUserId(request.CourseId, request.UserId);
            return progress.Select(x => x.ElementId).ToList();
        }
    }
}
