namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data
{
    internal class CategoryJsonModel
    {
        public string Name { get; set; }
        public List<SubcategoryJsonModel> Subcategories { get; set; }
    }
}
