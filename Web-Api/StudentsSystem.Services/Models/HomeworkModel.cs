namespace StudentsSystem.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    
    using StudentSystem.Models;

    public class HomeworkModel
    {
        public static Expression<Func<Homework, HomeworkModel>> FromHomework
        {
            get
            {
                return h => new HomeworkModel
                {
                    Id = h.Id,
                    Deadline = h.Deadline,
                    TimeSent = h.TimeSent
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }
    }
}