using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class AddCourseHandler : IRequestHandler<AddCourseRequest>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddCourseHandler(ICourseRepository courseRepository, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }
        public async Task Handle(AddCourseRequest request, CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                Info = new CourseInfo()
                {
                    Title = request.Title,
                    Subtitle = request.Subtitle,
                },
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                ThumbnailUrl = request.ThumbnailUrl,
            };

            await _courseRepository.Add(course);
        }
    }
}
