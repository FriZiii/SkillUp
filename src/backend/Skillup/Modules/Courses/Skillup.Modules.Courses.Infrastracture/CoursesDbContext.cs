﻿using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Entities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture
{
    internal class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("courses");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}