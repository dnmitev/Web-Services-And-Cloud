namespace App.Data.Contracts
{
    using System;
    using System.Linq;

    using App.Models;

    public interface IAppData
    {
        // TODO: List all repos
        IGenericRepository<User> Users { get; }

        void SaveChanges();
    }
}