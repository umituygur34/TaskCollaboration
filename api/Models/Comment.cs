using System;

namespace TaskCollaboration.Api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Hangi User yaptı? 
        public int UserId { get; set; }
        public User? User { get; set; }

        //Hangi Task'ta yapıldı ?
        public int WorkTaskId { get; set; }
        public WorkTask? Task { get; set; }
    }
}