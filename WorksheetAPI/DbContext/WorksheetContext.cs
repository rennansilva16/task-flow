
using Microsoft.EntityFrameworkCore;
using WorksheetData.Models;

public class WorksheetContext : DbContext
{
    public WorksheetContext(DbContextOptions<WorksheetContext> options) : base(options) { }

    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<TipoDeTarefa> TipoDeTarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
