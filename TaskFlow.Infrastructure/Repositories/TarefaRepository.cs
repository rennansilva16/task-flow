using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Persistence;

namespace TaskFlow.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TaskFlowDbContext context;

    public TarefaRepository(TaskFlowDbContext context)
    {
        this.context = context;
    }

    public async Task<Tarefa> CreateTask(Tarefa tarefa)
    {
            await context.AddAsync(tarefa);
            await context.SaveChangesAsync();
            return tarefa;
    }

    public async Task<Tarefa?> UpdateTask(Tarefa tarefa, long usuarioId)
    {
        var tarefaExistente = await context.Tarefas
            .FirstOrDefaultAsync(t => t.Id == tarefa.Id && t.UsuarioId == usuarioId);

        if (tarefaExistente == null)
            return null;

        tarefaExistente.Titulo = tarefa.Titulo;
        tarefaExistente.Descricao = tarefa.Descricao;
        tarefaExistente.DataPrazo = tarefa.DataPrazo;
        tarefaExistente.Status = tarefa.Status;

        await context.SaveChangesAsync();

        return tarefaExistente;
    }

    public async Task<bool> DeleteTask(long id, long usuarioId)
    {
        var tarefa = await context.Tarefas
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

        if (tarefa == null)
            return false;

        context.Remove(tarefa);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Tarefa>> GetAllTasks(long usuarioId)
    {
        return await context.Tarefas
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Tarefa?> GetTaskById(long id, long usuarioId)
    {
        return await context.Tarefas
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);
    }
}
