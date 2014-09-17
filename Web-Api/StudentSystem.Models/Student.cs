namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Student
    {
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks;

        public Student()
        {
            this.Id = Guid.NewGuid();
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();
        }

        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Course> Courses
        {
            get
            {
                return this.courses;
            }

            set
            {
                this.courses = value;
            }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get
            {
                return this.homeworks;
            }

            set
            {
                this.homeworks = value;
            }
        }
    }
}