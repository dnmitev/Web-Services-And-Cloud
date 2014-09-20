namespace MusicSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
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