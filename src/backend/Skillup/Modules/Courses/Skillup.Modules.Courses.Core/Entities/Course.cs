using System.ComponentModel.DataAnnotations.Schema;

namespace Skillup.Modules.Courses.Core.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        //public Guid AuthorId { get; set; }      //id autora
        //public Author Author { get; set; }
        public CourseTextSection TextSection { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }    // kategoria kursu np. lanuages
                                                  // 
        [ForeignKey(nameof(Subcategory))]
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }  // podkategoria kursu np. english
        public CourseLevel CourseLevel { get; set; }     //poziom kursu np. advanced/ beginner

    }
}
