namespace Articles.Services.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    
    using Articles.Models;

    public class ArticleDetailsDataModel
    {
        public ArticleDetailsDataModel(Article article)
        {
            this.Id = article.Id;
            this.Title = article.Title;
            this.Content = article.Content;
            this.Category = article.Category.Name;
            this.DateCreated = article.DateCreated;
            this.Tags = article.Tags
                               .Select(t => t.Name)
                               .ToArray();
            this.Comments = article.Comments
                                   .AsQueryable()
                                   .Select(CommentDataModel.FromComment)
                                   .Take(10)
                                   .ToArray();
            this.Likes = article.Likes
                                .AsQueryable()
                                .Select(LikeDataModel.FromLike)
                                .ToArray();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<string> Tags { get; set; }

        public ICollection<CommentDataModel> Comments { get; set; }

        public ICollection<LikeDataModel> Likes { get; set; }
    }
}