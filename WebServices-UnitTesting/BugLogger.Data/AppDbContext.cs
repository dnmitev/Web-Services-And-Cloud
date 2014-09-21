namespace BugLogger.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using BugLogger.Data.Contracts;
    using BugLogger.Models;
    using BugLogger.Data.Migrations;

    public class AppDbContext : DbContext, IAppDBContext
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        public IDbSet<Bug> Bugs { get; set; }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        
        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}