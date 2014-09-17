namespace StudentSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using StudentSystem.Data.Contracts;
    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public class StudentSystemData : IStudentSystemData
    {
        private readonly IStudentSystemDBContext context;
        private readonly IDictionary<Type, object> repositories;

        public StudentSystemData()
            : this(new StudentSystemDbContext())
        {
        }

        public StudentSystemData(IStudentSystemDBContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Student> Students
        {
            get
            {
                return this.GetRepository<Student>();
            }
        }

        public IGenericRepository<Course> Courses
        {
            get
            {
                return this.GetRepository<Course>();
            }
        }

        public IGenericRepository<Homework> Homeworks
        {
            get
            {
                return this.GetRepository<Homework>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}