namespace App.Data.Contracts
{
    using System;
    using System.Linq;

    using App.Models;

    public interface IAppData
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<Game> Games { get; }

        IGenericRepository<Guess> Guesses { get; }

        IGenericRepository<Notification> Notifications { get; }

        void SaveChanges();
    }
}