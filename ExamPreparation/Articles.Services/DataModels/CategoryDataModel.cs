namespace Articles.Services.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    
    using Articles.Models;

    public class CategoryDataModel
    {
        public static Expression<Func<Category, CategoryDataModel>> FromCategory
        {
            get
            {
                return c => new CategoryDataModel
                {
                    Id = c.Id,
                    Name = c.Name
                };
            }
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}