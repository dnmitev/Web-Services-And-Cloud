namespace MusicSystem.Data.Contracts
{
    using System;
    using System.Linq;

    using MusicSystem.Models;

    public interface IMusicSystemData
    {
        IGenericRepository<Artist> Artists { get; }

        IGenericRepository<Album> Albums { get; }

        IGenericRepository<Song> Songs { get; }

        void SaveChanges();
    }
}