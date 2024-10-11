using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class AddCourseHandler : IRequestHandler<AddCourseRequest>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IClock _clock;

        public AddCourseHandler(ICourseRepository courseRepository, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository, IClock clock)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _clock = clock;
        }
        public async Task Handle(AddCourseRequest request, CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                //TODO: AuthorId = request.AuthorId
                Title = request.Title,
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                CreatedAt = _clock.CurrentDate()
            };

            await _courseRepository.Add(course);

            request.CourseID = course.Id;
        }
    }
}
