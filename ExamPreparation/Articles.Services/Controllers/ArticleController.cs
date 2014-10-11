namespace Articles.Services.Controllers
{
    using Articles.Data;
    using Articles.Data.Contracts;
    using Articles.Models;
    using Articles.Services.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using System.Web.Http;

    public class ArticleController : BaseApiController
    {
        private const int DefaultPageSize = 10;

        //// TODO: IoC -> Ninject
        //public ArticleController()
        //    : this(new AppData())
        //{
        //}

        public ArticleController(IAppData data) : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult All(string category, int page)
        {
            var articles = this.GetOrderedByDate()
                               .Where(a => category != null ? a.Category.Name.Equals(category, StringComparison.InvariantCultureIgnoreCase) : true)
                               .Skip(DefaultPageSize * page)
                               .Take(DefaultPageSize)
                               .Select(ArticleDataModel.FromArticle);

            return Ok(articles);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult All(string category)
        {
            return this.All(category, 0);
        }

        [HttpGet]
        public IHttpActionResult All(int page)
        {
            return this.All(null, page);
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            return this.All(null, 0);
        }

        [HttpGet]
        public IHttpActionResult GetByID(int articleId)
        {
            var article = this.Data.Articles
                              .All()
                              .FirstOrDefault(a => a.Id == articleId);

            if (article == null)
            {
                return NotFound();
            }

            var articleDetails = new ArticleDetailsDataModel(article);

            return Ok(articleDetails);
        }

        [HttpPost]
        [Authorize]
        [Route("api/articles")]
        public IHttpActionResult Create(ArticleDataModel article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = this.User.Identity.GetUserId();

            var category = this.GetCategory(article);
            var tags = this.GetTags(article);

            var articleToAdd = new Article
            {
                Title = article.Title,
                Content = article.Content,
                DateCreated = DateTime.Now,
                AuthorId = userId,
                CategoryId = category.Id,
                Tags = tags
            };

            this.Data.Articles.Add(articleToAdd);
            this.Data.SaveChanges();

            article.Id = articleToAdd.Id;
            article.DateCreated = articleToAdd.DateCreated;
            article.Tags = articleToAdd.Tags.Select(t => t.Name);

            return Ok(article);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult CraateCommentOnArticle(int articleId, CommentDataModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = this.Data.Articles
                              .All()
                              .FirstOrDefault(a => a.Id == articleId);

            if (article == null)
            {
                return NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            var commentToAdd = new Comment
            {
                Content = comment.Content,
                DateCreated = DateTime.Now,
                ArticleId = article.Id,
                AuthorId = userId
            };

            article.Comments.Add(commentToAdd);
            this.Data.SaveChanges();

            comment.Id = commentToAdd.Id;
            return Ok(comment);
        }

        private HashSet<Tag> GetTags(ArticleDataModel article)
        {
            HashSet<Tag> tags = new HashSet<Tag>();
            var newTags = article.Tags.ToList();
            var tagsFromTitle = article.Title.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            newTags.AddRange(tagsFromTitle);

            foreach (var tagName in newTags)
            {
                var tag = this.Data.Tags
                              .All()
                              .FirstOrDefault(t => t.Name == tagName);

                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    this.Data.Tags.Add(tag);
                }

                tags.Add(tag);
            }

            return tags;
        }

        private Category GetCategory(ArticleDataModel article)
        {
            var category = this.Data.Categories
                               .All()
                               .FirstOrDefault(c => c.Name == article.Category);

            if (category == null)
            {
                category = new Category { Name = article.Category };
                this.Data.Categories.Add(category);
            }

            return category;
        }

        private IQueryable<Article> GetOrderedByDate()
        {
            return this.Data.Articles
                       .All()
                       .OrderBy(a => a.DateCreated);
        }
    }
}