namespace BugLogger.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Bug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string LogText { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime LogDate { get; set; }
    }
}