namespace Articles.Services.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    
    using Articles.Models;

    public class CommentDataModel
    {
        public static Expression<Func<Comment, CommentDataModel>> FromComment
        {
            get
            {
                return c => new CommentDataModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    DateCreated = c.DateCreated,
                    AuthorName = c.Author.UserName
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public string AuthorName { get; set; }
    }
}