﻿using Skillup.Shared.Abstractions.S3;
using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseModuleSeeder : ISeeder
    {

        private readonly CoursesDbContext _context;
        private readonly IClock _clock;
        private readonly IAmazonS3Service _s3Service;

        public CourseModuleSeeder(CoursesDbContext context, IClock clock, IAmazonS3Service s3Service)
        {
            _context = context;
            _clock = clock;
            _s3Service = s3Service;
        }

        public async Task Seed()
        {
            var _categoriesSeeder = new CategorySeeder(_context);
            await _categoriesSeeder.Seed();

            var _userSeeder = new CourseUserSeeder(_context, _s3Service);
            await _userSeeder.Seed();

            var _courseSeeder = new CourseSeeder(_context, _clock, _s3Service);
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
