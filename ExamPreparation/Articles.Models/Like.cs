namespace Articles.Models
{
    using System;

    public class Like
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserId { get; set; }

        public virtual User  User { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}