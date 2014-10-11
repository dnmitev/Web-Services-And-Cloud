namespace Articles.Data.Contracts
{
    using System;
    using System.Linq;

    using Articles.Models;

    public interface IAppData
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<Article> Articles { get; }

        IGenericRepository<Comment> Comments { get; }

        IGenericRepository<Category> Categories{ get; }

        IGenericRepository<Tag> Tags { get; }

        IGenericRepository<Like> Likes { get; }

        IGenericRepository<Alert> Alerts { get; }

        void SaveChanges();
    }
}