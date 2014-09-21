namespace BugLogger.Data.Contracts
{
    using System;
    using System.Linq;

    using BugLogger.Models;

    public interface IAppData
    {
        IGenericRepository<Bug> Bugs { get; }

        void SaveChanges();
    }
}