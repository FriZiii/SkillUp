using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.Seeder;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CategorySeeder : ISeeder
    {
        private readonly CoursesDbContext _context;
        private DbSet<Category> _categories;
        private DbSet<Subcategory> _subCategories;

        public CategorySeeder(CoursesDbContext context)
        {
            _context = context;
            _categories = _context.Categories;
            _subCategories = _context.Subcategories;
        }

        public async Task Seed()
        {
            if (!await _categories.AnyAsync() && !await _subCategories.AnyAsync())
            {
                await SeedCategories();
            }
        }

        private async Task SeedCategories()
        {
            var categoriesToAdd = new List<Category>();
            var subcategoriesToAdd = new List<Subcategory>();
            var categoriesWithSubcategories = new Dictionary<string, List<string>>()
            {
                { "Languages", [ "English", "German", "Spanish", "French", "Other" ] },
                { "Programming", ["Basics of programming", "Game development", "Web development", "Mobile development", "Databases", "Artificial intelligence", "Cybersecurity", "Other" ] },
                { "Self-development", [ "Professional development", "Motivation", "Health", "Sports", "Creativity & hobbies", "Other" ] },
                { "Business and managing",[ "Project management", "Marketing", "Finance", "HR & recruitment", "Other" ] },
                { "Science",[ "Mathematics", "Physics", "Biology", "Chemistry", "Engineering", "Other" ] },
                { "Mathematics",[ "Algebra", "Analysis", "Statistics", "Probability", "Discrete mathematics" ] },
                { "Humanities",[ "Philosophy", "History", "Art history", "Literature", "Culture", "Religions", "Law", "Other" ] },
                { "Art",[ "Literature", "Music", "Visual arts", "Other" ] },
                { "Teaching",[ "Teaching methods", "Online teaching", "Teaching children & teenagers", "Other" ] },
                { "Applications",[ "Microsoft", "Apple", "Autodesk", "Adobe", "Google", "Other" ] }
            };

            foreach (var categoryWithSubcategory in categoriesWithSubcategories)
            {
                var category = new Category
                {
                    Name = categoryWithSubcategory.Key
                };
                categoriesToAdd.Add(category);

                foreach (var subCategoryName in categoryWithSubcategory.Value)
                {
                    var subCategory = new Subcategory
                    {
                        Name = subCategoryName,
                        Category = category
                    };
                    subcategoriesToAdd.Add(subCategory);
                }
            }

            await _categories.AddRangeAsync(categoriesToAdd);
            await _subCategories.AddRangeAsync(subcategoriesToAdd);
            await _context.SaveChangesAsync();
        }
    }
}