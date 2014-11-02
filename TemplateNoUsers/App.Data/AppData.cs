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
        private readonly IAppDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public AppData()
            : this(new ApplicationDbContext())
        {
        }

        public AppData(IAppDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        // TODO: Register model repositories
        public IGenericRepository<Model> Models
        {
            get
            {
                return this.GetRepository<Model>();
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