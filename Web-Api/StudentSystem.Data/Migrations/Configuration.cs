namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using StudentSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            this.SeedStudents(context);
            this.SeedCourses(context);
        }

        private void SeedStudents(StudentSystemDbContext context)
        {
            var students = new List<Student>()
            {
                new Student
                {
                    FirstName = "Dinko",
                    LastName = "Spindermanov",
                    PhoneNumber = "0888123321"
                },
                new Student
                {
                    FirstName = "Bat",
                    LastName = "Spinder",
                    PhoneNumber = "0989212121"
                },
                new Student
                {
                    FirstName = "Bat",
                    LastName = "Gubi-Buklik",
                    PhoneNumber = "026559944"
                }
            };

            foreach (var st in students)
            {
                context.Students.Add(st);
            }
        }

        private void SeedCourses(StudentSystemDbContext context)
        {
            var courses = new List<Course>()
            {
                new Course
                {
                    Name = "Learn to be piinyak",
                    Description = "Kogaaaa"
                },
                new Course
                {
                    Name = "Sucky-fucky banana",
                    Description = "Bat Ivo e nai-golemiq"
                }
            };

            foreach (var c in courses)
            {
                context.Courses.Add(c);                
            }
        }
    }
}