using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseModuleSeeder : ISeeder
    {

        private readonly CoursesDbContext _context;
        private readonly IClock _clock;
        public CourseModuleSeeder(CoursesDbContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        public async Task Seed()
        {
            var _categoriesSeeder = new CategorySeeder(_context);
            await _categoriesSeeder.Seed();

            var _userSeeder = new CourseUserSeeder(_context);
            await _userSeeder.Seed();

            var _courseSeeder = new CourseSeeder(_context, _clock);
            await _courseSeeder.Seed();

            var _sectionsSeeder = new SectionsSeeder(_context);
            await _sectionsSeeder.Seed();

            var _elementsSeeder = new ElementsSeeder(_context);
            await _elementsSeeder.Seed();

            var _exerciseSeeder = new ExerciseSeeder(_context);
            await _exerciseSeeder.Seed();
        }
    }
}
