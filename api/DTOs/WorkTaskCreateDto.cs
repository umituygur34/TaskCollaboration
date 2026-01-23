using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskCollaboration.Api.api.DTO
{
    public class WorkTaskCreateDto
    {
        public string Title{get;  set;} = string.Empty; //Görev başlığı
        public string Description{get; set;} = string.Empty; //Detaylı açıklama
        public string Status {get; set;} = string.Empty;
        public string Priority {get; set;} = string.Empty; 
        public DateTime DueDate {get; set;} = DateTime.UtcNow; 
        
    }
}