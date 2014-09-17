namespace StudentsSystem.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    
    using StudentSystem.Models;

    public class StudentModel
    {
        public static Expression<Func<Student, StudentModel>> FromStudent
        {
            get
            {
                return s => new StudentModel
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName
                };
            }
        }

        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
    }
}