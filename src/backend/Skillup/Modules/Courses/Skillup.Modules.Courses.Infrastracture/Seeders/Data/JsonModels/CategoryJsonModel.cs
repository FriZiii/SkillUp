namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels
{
    internal class CategoryJsonModel
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public List<SubcategoryJsonModel> Subcategories { get; set; }
    }
}
