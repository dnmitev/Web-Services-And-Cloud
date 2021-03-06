﻿namespace MusicSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MusicSystem.Data.Contracts;
    using MusicSystem.Data.Repositories;
    using MusicSystem.Models;

    public class MusicSystemData : IMusicSystemData
    {
        private readonly IMusicSystemDBContext context;
        private readonly IDictionary<Type, object> repositories;

        public MusicSystemData()
            : this(new MusicSystemDbContext())
        {
        }

        public MusicSystemData(IMusicSystemDBContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Artist> Artists
        {
            get
            {
                return this.GetRepository<Artist>();
            }
        }

        public IGenericRepository<Album> Albums
        {
            get
            {
                return this.GetRepository<Album>();
            }
        }

        public IGenericRepository<Song> Songs
        {
            get
            {
                return this.GetRepository<Song>();
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