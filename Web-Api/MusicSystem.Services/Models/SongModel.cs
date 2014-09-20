namespace MusicSystem.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    
    using MusicSystem.Models;

    public class SongModel
    {
        public static Expression<Func<Song,SongModel>> FromSong
        {
            get
            {
                return s => new SongModel
                {
                    Title = s.Title,
                    Year = s.Year,
                    Genre = s.Genre
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}