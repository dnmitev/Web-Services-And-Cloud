namespace App.Data
{
    using System.Data.Entity;

    using App.Data.Contracts;
    using App.Data.Migrations;
    using App.Models;

    public class ApplicationDbContext : DbContext, IAppDbContext
    {
        public ApplicationDbContext()
            : this("DefaultConnection")
        {
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
            // TODO: Set migrations config
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        // TODO: List all IDbSet of Models
        public IDbSet<Model> Models { get; set; }

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