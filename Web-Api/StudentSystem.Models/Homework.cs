namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Homework
    {
        private DateTime deadline;
        private DateTime timeSent;

        public int Id { get; set; }

        [Required]
        public DateTime Deadline
        {
            get
            {
                return DateTime.Parse(this.deadline.ToString());
            }

            set
            {
                this.deadline = value;
            }
        }

        [Required]
        public DateTime TimeSent
        {
            get
            {
                return DateTime.Parse(this.timeSent.ToString());
            }

            set
            {
                this.timeSent = value;
            }
        }

        public string FileName { get; set; }
    }
}