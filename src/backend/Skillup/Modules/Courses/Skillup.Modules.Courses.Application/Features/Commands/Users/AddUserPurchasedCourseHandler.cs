using MediatR;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Users;

namespace Skillup.Modules.Courses.Application.Features.Commands.Users
{
    internal class AddUserPurchasedCourseHandler : IRequestHandler<AddUserPurchasedCourseRequest>
    {
        private readonly IUserPurchasedCourseRepository _userPurchasedCourseRepository;

        public AddUserPurchasedCourseHandler(IUserPurchasedCourseRepository userPurchasedCourseRepository)
        {
            _userPurchasedCourseRepository = userPurchasedCourseRepository;
        }

        public async Task Handle(AddUserPurchasedCourseRequest request, CancellationToken cancellationToken)
        {
            await _userPurchasedCourseRepository.Add(new UserPurchasedCourse(request.UserId, request.CourseId));
        }
    }
}
