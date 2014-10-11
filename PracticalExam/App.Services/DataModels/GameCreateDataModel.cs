namespace App.Services.DataModels
{
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    
    public class GameCreateDataModel
    {
        public static Expression<Func<Game,GameCreateDataModel>> FromGame
        {
            get
            {
                return g => new GameCreateDataModel
                {
                    Id = g.Id,
                    Name = g.Name
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(1000,9999)]
        public int Number { get; set; }
    }
}