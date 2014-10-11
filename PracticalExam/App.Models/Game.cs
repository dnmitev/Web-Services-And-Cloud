namespace App.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string FirstUserId { get; set; }

        [Range(1000,9999)]
        public int FirstUserSecretNumber { get; set; }

        public virtual User FirstUser { get; set; }

        public string SecondUserId { get; set; }

        [Range(1000, 9999)]
        public int? SecondUserSecretNumber { get; set; }

        public virtual User SecondUser { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}