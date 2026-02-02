

namespace TaskCollaboration.Api.DTOs.ActivityLogDto
{
    public class ActivityLogDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int? WorkTaskId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}