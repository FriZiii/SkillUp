using MediatR;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Application.Managments.Course.Commands
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
            var category = await _categoryRepository.GetById(request.CategoryId);
            var subcategory = await _subcategoryRepository.GetById(request.SubcategoryId);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (subcategory == null)
            {
                throw new ArgumentNullException();
            }
            var course = new Core.Entities.Course()
            {
                Info = new CourseInfo()
                {
                    Title = request.Title,
                    Subtitle = request.Subtitle,
                },
                Category = category,
                Subcategory = subcategory,
                ThumbnailUrl = request.ThumbnailUrl,
            };

            try
            {
                await _courseRepository.Add(course);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
