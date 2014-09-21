namespace BugLogger.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using BugLogger.Models;
    using System.Linq.Expressions;
    
    public class BugModel
    {
        public static Expression<Func<Bug, BugModel>> BugTemplate
        {
            get
            {
                return b => new BugModel
                {
                    LogText = b.LogText,
                    LogDate = b.LogDate,
                    Status = b.Status
                };
            }
        }

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