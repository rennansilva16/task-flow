using Microsoft.EntityFrameworkCore;
using TaskFlowData.Models;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }

    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
