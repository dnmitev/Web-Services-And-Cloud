namespace MusicSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using MusicSystem.Data.Contracts;
    using MusicSystem.Data.Migrations;
    using MusicSystem.Models;

    public class MusicSystemDbContext : DbContext, IMusicSystemDBContext
    {
        public MusicSystemDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicSystemDbContext, Configuration>());
        }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Song> Songs { get; set; }

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