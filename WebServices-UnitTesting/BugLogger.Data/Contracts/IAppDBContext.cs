namespace BugLogger.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using BugLogger.Models;

    public interface IAppDBContext
    {
        IDbSet<Bug> Bugs { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}