namespace MusicSystem.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using MusicSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class AlbumModel
    {
        public static Expression<Func<Album, AlbumModel>> FromAlbum
        {
            get
            {
                return al => new AlbumModel
                {
                    Title = al.Title,
                    Year = al.Year
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }
    }
}