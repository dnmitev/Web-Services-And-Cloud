namespace StudentsSystem.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    
    using StudentSystem.Models;

    public class CourseModel
    {
        public static Expression<Func<Course, CourseModel>> FromCourse
        {
            get
            {
                return c => new CourseModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}