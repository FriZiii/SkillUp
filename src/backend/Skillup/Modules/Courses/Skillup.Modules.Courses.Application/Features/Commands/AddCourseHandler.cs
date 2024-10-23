using MassTransit;
using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.Events.Finances;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class AddCourseHandler : IRequestHandler<AddCourseRequest>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IClock _clock;
        private readonly IPublishEndpoint _publishEndpoint;

        public AddCourseHandler(ICourseRepository courseRepository, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository, IClock clock, IPublishEndpoint publishEndpoint)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _clock = clock;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(AddCourseRequest request, CancellationToken cancellationToken)
        {
            var course = new Course(request.AuthorId, request.Title, request.CategoryId, request.SubcategoryId, _clock.CurrentDate());

            await _courseRepository.Add(course);

            request.CourseID = course.Id;

            await _publishEndpoint.Publish(new CourseAdded(course.Id, request.AuthorId));
        }
    }
}
