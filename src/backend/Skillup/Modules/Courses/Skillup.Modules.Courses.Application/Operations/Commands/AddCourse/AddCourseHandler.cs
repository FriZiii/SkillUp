using MediatR;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Application.Operations.Commands.AddCourse
{
    public class AddCourseHandler : IRequestHandler<AddCourse>
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
        public async Task Handle(AddCourse request, CancellationToken cancellationToken)
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
