using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetCoursesHandler : IRequestHandler<GetCoursesRequest, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IUserPurchasedCourseRepository _userPurchasedCourseRepository;

        public GetCoursesHandler(ICourseRepository courseRepository, IUserRepository userRepository, IAmazonS3Service amazonS3Service, IUserPurchasedCourseRepository userPurchasedCourseRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _amazonS3Service = amazonS3Service;
            _userPurchasedCourseRepository = userPurchasedCourseRepository;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseMapper(_amazonS3Service);
            IEnumerable<Course?> courses;
            if (request.Status == null)
            {
                courses = await _courseRepository.GetAll();
            }
            else
            {
                courses = await _courseRepository.GetByStatus((CourseStatus)request.Status);
            }

            var purchasedCourses = await _userPurchasedCourseRepository.Get();
            var purchasedCoursesByCourse = purchasedCourses.GroupBy(x => x.CourseId);

            var coursesDtos = courses.Select(mapper.CourseToCourseDto).ToList();

            var users = await _userRepository.GetAll();

            foreach (var courseDto in coursesDtos)
            {
                courseDto.UsersCout = purchasedCoursesByCourse.Select(x => x.Where(x => x.CourseId == courseDto.Id)).Count();
                courseDto.AuthorName = users.First(u => u.Id == courseDto.AuthorId).FirstName + " " + users.First(u => u.Id == courseDto.AuthorId).LastName;
            }

            return coursesDtos;
        }
    }
}
