namespace App.Data.Contracts
{
    using System;
    using System.Linq;

    using App.Models;

    public interface IAppData
    {
        // TODO: List all repositories
        IGenericRepository<Model> Models { get; }

        void SaveChanges();
    }
}