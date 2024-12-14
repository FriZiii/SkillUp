using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetProgressForUserCoursesHandler(ICourseUserProgressRepository courseUserProgressRepository, ICourseRepository courseRepository) : IRequestHandler<GetProgressForUserCoursesRequest, IEnumerable<CoursePercentageProgressDto>>
    {
        private readonly ICourseUserProgressRepository _courseUserProgressRepository = courseUserProgressRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;

        public async Task<IEnumerable<CoursePercentageProgressDto>> Handle(GetProgressForUserCoursesRequest request, CancellationToken cancellationToken)
        {
            var progress = await _courseUserProgressRepository.GetByUserId(request.UserId);

            var progressByCourse = progress.GroupBy(x => x.CourseId);

            var result = await Task.WhenAll(progressByCourse.Select(async courseProgress =>
            {
                var mapper = new CourseProgressMapper();
                return await mapper.CourseProgressToPercetageProgressDtoMapper(courseProgress, _courseRepository);
            }));

            return result;
        }
    }
}
