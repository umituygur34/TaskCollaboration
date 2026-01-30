using Microsoft.EntityFrameworkCore;
using TaskCollaboration.Api.Models;


namespace TaskCollaboration.Api.Data;

public class TaskCollaborationDbContext : DbContext
{
    public TaskCollaborationDbContext(DbContextOptions<TaskCollaborationDbContext> options)
    : base(options)
    {


    }

    public DbSet<User> Users { get; set; }
    public DbSet<WorkTask> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

}