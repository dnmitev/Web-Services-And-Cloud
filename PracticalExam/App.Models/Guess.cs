namespace App.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public class Guess
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public virtual User User { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Required]
        [Range(1000, 9999)]
        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }
    }
}