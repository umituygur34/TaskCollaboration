using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCollaboration.Api.api.Models.Enums;

namespace TaskCollaboration.Api.api.DTOs
{
    public class WorkTaskDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; //Görev başlığı
        public string Description { get; set; } = string.Empty; //Detaylı açıklama
        public WorkTaskStatus Status { get; set; }
        public WorkTaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;




    }
}