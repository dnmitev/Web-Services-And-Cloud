namespace StudentSystem.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using StudentSystem.Models;

    public interface IStudentSystemDBContext
    {
        IDbSet<Course> Courses { get; set; }

        IDbSet<Student> Students { get; set; }

        IDbSet<Homework> Homeworks { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}