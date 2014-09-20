namespace MusicSystem.Services.Models
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using MusicSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class ArtistModel
    {
        public static Expression<Func<Artist, ArtistModel>> FromArtist
        {
            get
            {
                return ar => new ArtistModel
                {
                    Name = ar.Name,
                    Country = ar.Country,
                    DateOfBirth = ar.DateOfBirth
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}