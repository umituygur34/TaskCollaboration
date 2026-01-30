namespace TaskCollaboration.Api.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }

        //"Add", "Update", "Delete"
        public string Action { get; set; } = string.Empty;

        //Detaylı açıklama
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //Oluşturma tarihi


        //Hangi User yaptı? 
        public int UserId { get; set; }
        public User? User { get; set; }

        //Hangi Task'ta yapıldı ?
        public int? TaskId { get; set; }
        public WorkTask? Task { get; set; }

    }
}