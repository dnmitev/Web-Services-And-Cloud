﻿namespace App.Data.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using App.Models;

    public interface IAppDbContext
    {
        // TODO: List all models db sets
        IDbSet<Model> Models { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}