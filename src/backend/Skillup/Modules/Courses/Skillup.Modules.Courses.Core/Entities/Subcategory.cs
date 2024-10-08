﻿namespace Skillup.Modules.Courses.Core.Entities
{
    public class Subcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Course> Courses { get; set; }

    }
}