namespace StudentSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using StudentSystem.Data;
    using StudentSystem.Models;
    using System.Globalization;
    using System.Collections.Generic;

    internal class Client
    {
        private static void Main()
        {
            var data = new StudentSystemData();

            var hw = new Homework
            {
                FileName = "Domestic rakia",
                Deadline = new DateTime(2014, 08, 31),
                TimeSent = new DateTime(2014, 08, 29)
            };

            var student = new Student
            {
                FirstName = "Pesho",
                LastName = "Piandeto",
                PhoneNumber = "082569875",
                Homeworks = new List<Homework> { hw }
            };

            data.Homeworks.Add(hw);

            data.Students.Add(student);

            data.Courses.Add(new Course
            {
                Name = "Kukuruz seller",
                Description = "Corn, corn, corn",
                Students = new List<Student> { student }
            });

            data.SaveChanges();

        }
    }
}