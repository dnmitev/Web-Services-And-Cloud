namespace Articles.Services.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    
    using Articles.Models;

    public class ArticleDataModel
    {
        private IEnumerable<string> tags;

        public ArticleDataModel()
        {
            this.Tags = new HashSet<string>();
        }

        public static Expression<Func<Article, ArticleDataModel>> FromArticle
        {
            get
            {
                return a => new ArticleDataModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.Category.Name,
                    DateCreated = a.DateCreated,
                    Tags = a.Tags.Select(t => t.Name)
                };
            }
        }

        [Key]
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

        public IEnumerable<string> Tags
        {
            get
            {
                return this.tags;
            }

            set
            {
                this.tags = value;
            }
        }
    }
}