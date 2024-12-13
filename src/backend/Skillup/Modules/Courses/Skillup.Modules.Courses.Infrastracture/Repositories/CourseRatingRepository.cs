﻿using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CourseRatingRepository : ICourseRatingRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<CourseRating> _ratings;

        public CourseRatingRepository(CoursesDbContext context)
        {
            _context = context;
            _ratings = context.CourseRatings;
        }

        public async Task Add(CourseRating courseRating)
        {
            await _ratings.AddAsync(courseRating);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CourseRating courseRating)
        {
            var ratingToEdit = await _ratings.FirstOrDefaultAsync(x => x.Id == courseRating.Id) ?? throw new Exception(); // TODO: Custom ex: rating with id doesnt exist
            ratingToEdit.Stars = courseRating.Stars;
            ratingToEdit.Feedback = courseRating.Feedback;
            ratingToEdit.Timestamp = courseRating.Timestamp;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseRating>> Get()
            => await _ratings.ToListAsync();

        public async Task<IEnumerable<CourseRating>> GetByCourseId(Guid courseId)
            => await _ratings.Where(x => x.CourseId == courseId).ToListAsync();

        public async Task<IEnumerable<CourseRating>> GetByUserId(Guid userId)
            => await _ratings.Where(x => x.RatedById == userId).ToListAsync();

        public async Task<CourseRating?> GetById(Guid id)
            => await _ratings.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Delete(Guid id)
        {
            var ratingToDelete = await _ratings.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception(); // TODO: Custom ex: rating with id doesnt exist
            _ratings.Remove(ratingToDelete);
            await _context.SaveChangesAsync();
        }
    }
}