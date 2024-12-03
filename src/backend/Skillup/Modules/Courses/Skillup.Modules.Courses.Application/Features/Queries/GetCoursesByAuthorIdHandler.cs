using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCoursesByAuthorIdHandler(ICourseRepository courseRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetCoursesByAuthorIdRequest, List<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<List<CourseDto>> Handle(GetCoursesByAuthorIdRequest request, CancellationToken cancellationToken)
        {
            var courses = (await _courseRepository.GetAll()).Where(c => c.AuthorId == request.authorID);
            var mapper = new CourseMapper(_amazonS3Service);
            return courses.Select(mapper.CourseToCourseDto).ToList();
        }
    }
}
