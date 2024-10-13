using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data;
using Skillup.Shared.Abstractions.Seeder;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CategorySeeder : ISeeder
    {
        private readonly CoursesDbContext _context;
        private DbSet<Category> _categories;
        private DbSet<Subcategory> _subCategories;
        private JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        private List<CategoryJsonModel> categoryData = new();
        private List<Category> _categoriesList = new();

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

            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data", "JsonFiles");

            var jsonString = File.ReadAllText(Path.Combine(path, "category-seeder-data.json"));
            categoryData = JsonSerializer.Deserialize<List<CategoryJsonModel>>(jsonString, _jsonSerializerOptions);

            await _categories.AddRangeAsync(CreateCategories(categoryData!));
            await _context.SaveChangesAsync();
            _categoriesList = await _categories.ToListAsync();

            await _subCategories.AddRangeAsync(CreateSubcategories(categoryData!));
            await _context.SaveChangesAsync();
        }

        public List<Category> CreateCategories(List<CategoryJsonModel> categoryData)
        {
            return categoryData!.Select(CreateCategoryFromJson).ToList();
        }

        private Category CreateCategoryFromJson(CategoryJsonModel jsonModel)
        {
            return new Category() { Name = jsonModel.Name };
        }

        public IEnumerable<Subcategory> CreateSubcategories(List<CategoryJsonModel> categoryData)
        {
            var subcategories = new List<Subcategory>();

            foreach (var category in categoryData!)
            {
                foreach (var subcategory in category.Subcategories)
                {
                    subcategories.Add(new Subcategory() { Name = subcategory.Name, CategoryId = _categoriesList.First(x => x.Name == category.Name).Id });
                }
            }

            return subcategories;
        }
    }
}