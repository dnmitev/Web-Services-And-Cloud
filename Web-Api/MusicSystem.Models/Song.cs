namespace MusicSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Genre { get; set; }

        public int? ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}