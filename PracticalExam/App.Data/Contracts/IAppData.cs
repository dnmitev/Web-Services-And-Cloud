namespace App.Data.Contracts
{
    using System;
    using System.Linq;

    using App.Models;

    public interface IAppData
    {
        IGenericRepository<ApplicationUser> Users { get; }

        // TODO: IGenericRepository<Model> Models { get; }

        void SaveChanges();
    }
}