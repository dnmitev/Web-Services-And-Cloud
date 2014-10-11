namespace Articles.Services.DataModels
{
    using System;
    using System.Linq.Expressions;

    using Articles.Models;

    public class LikeDataModel
    {
        public static Expression<Func<Like, LikeDataModel>> FromLike
        {
            get
            {
                return l => new LikeDataModel
                {
                    Id = l.Id,
                    DateCreated = l.DateCreated,
                    AuthorName = l.User.UserName
                };
            }
        }

        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string AuthorName { get; set; }
    }
}