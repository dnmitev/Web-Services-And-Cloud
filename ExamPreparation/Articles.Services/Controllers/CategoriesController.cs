namespace Articles.Services.Controllers
{
    using Articles.Data;
    using Articles.Data.Contracts;
    using Articles.Services.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    public class CategoriesController : BaseApiController
    {
        //public CategoriesController()
        //    : this(new AppData())
        //{
        //}

        public CategoriesController(IAppData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var categories = this.Data.Categories
                .All()
                .Select(CategoryDataModel.FromCategory);

            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult GetArticlesByCategoryId(int id)
        {
            var articles = this.Data.Articles
                .All()
                .Where(a => a.CategoryId == id)
                .OrderBy(a => a.DateCreated)
                .Select(ArticleDataModel.FromArticle);

            if (articles == null)
            {
                return NotFound();
            }

            return Ok(articles);
        }
    }
}