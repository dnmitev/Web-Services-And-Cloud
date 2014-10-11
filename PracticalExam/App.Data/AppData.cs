namespace App.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using App.Data.Contracts;
    using App.Models;
    using App.Data.Repositories;

    /// <summary>
    /// Unit of work
    /// </summary>
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

        public IGenericRepository<Game> Games
        {
            get
            {
                return this.GetRepository<Game>();
            }
        }

        public IGenericRepository<Guess> Guesses
        {
            get
            {
                return this.GetRepository<Guess>();
            }
        }

        public IGenericRepository<Notification> Notifications
        {
            get
            {
                return this.GetRepository<Notification>();
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