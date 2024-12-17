using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCourseByUserIdHandler(IUserPurchasedCourseRepository userPurchasedCourseRepository, ICourseRepository courseRepository, IAmazonS3Service amazonS3Service, IUserRepository userRepository) : IRequestHandler<GetCourseByUserIdRequest, IEnumerable<CourseDto>>
    {
        private readonly IUserPurchasedCourseRepository _userPurchasedCourseRepository = userPurchasedCourseRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<CourseDto>> Handle(GetCourseByUserIdRequest request, CancellationToken cancellationToken)
        {
            var purchasedCourse = await _userPurchasedCourseRepository.GetByUserId(request.UserId);
            var mapper = new CourseMapper(_amazonS3Service);
            var allCourses = await _courseRepository.GetByStatus(CourseStatus.Published);
            var userPurshedCourses = allCourses.Where(x => purchasedCourse.Any(y => y.CourseId == x.Id));

            var coursesDtos = userPurshedCourses.Select(mapper.CourseToCourseDto).ToList();
            var users = await _userRepository.GetAll();
            coursesDtos.ForEach(c => c.AuthorName = users.First(u => u.Id == c.AuthorId).FirstName + " " + users.First(u => u.Id == c.AuthorId).LastName);

            return coursesDtos;
        }
    }
}
