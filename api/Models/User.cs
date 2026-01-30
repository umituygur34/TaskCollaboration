
namespace TaskCollaboration.Api.Models;

public class User
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    //şifrelenmiş halini tutacağım.
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //1 user => Many task => tercih meselesi List>Task> de kullanabilirdik. 
    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();




}