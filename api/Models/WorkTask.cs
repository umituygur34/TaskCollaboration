using TaskCollaboration.Api.api.Models.Enums;

namespace TaskCollaboration.Api.api.Models;

public class WorkTask{

    public int Id {get; set;}
    public string Title{get;  set;} = string.Empty; //Görev başlığı

    public string Description{get; set;} = string.Empty; //Detaylı açıklama

    public WorkTaskStatus Status{get; set;} = WorkTaskStatus.Todo;// "Todo", "Inprogress","Done"

    public WorkTaskPriority Priority{get; set;} = WorkTaskPriority.Low;// "Low", "Medium", "High"

    public DateTime DueDate{get; set;} = DateTime.UtcNow; // Bitiş tarihi

    public DateTime CreatedAt{get; set;} = DateTime.UtcNow;// Oluşturma tarihi

    public int UserId{get; set;} //Foreign Key - Bu Task Kime ait?

    public User? User{get; set;}  // Navigation Property - Bu Task Kime ait?

    //1 Task => Many Comments
    public ICollection<Comment> Comments{get; set;} = new List<Comment>();
       


    
}