namespace Articles.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Articles.Data.Contracts;
    using Articles.Data.Repositories;
    using Articles.Models;

    public class AppData : IAppData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        public AppData()
            : this(new ApplicationDbContext())
        {
        }

        public AppData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IGenericRepository<Article> Articles
        {
            get
            {
                return this.GetRepository<Article>();
            }
        }

        public IGenericRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IGenericRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IGenericRepository<Tag> Tags
        {
            get
            {
                return this.GetRepository<Tag>();
            }
        }

        public IGenericRepository<Like> Likes
        {
            get
            {
                return this.GetRepository<Like>();
            }
        }

        public IGenericRepository<Alert> Alerts
        {
            get
            {
                return this.GetRepository<Alert>();
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