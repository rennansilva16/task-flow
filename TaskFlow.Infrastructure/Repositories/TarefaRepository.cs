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

    public async Task<Tarefa> UpdateTask(Tarefa tarefa)
    {
        var tarefaExistente = await context.Tarefas.FindAsync(tarefa.Id);

        if (tarefaExistente == null)
            throw new Exception("Tarefa não encontrada.");

        tarefaExistente.Titulo = tarefa.Titulo;
        tarefaExistente.Descricao = tarefa.Descricao;
        tarefaExistente.DataPrazo = tarefa.DataPrazo;

        await context.SaveChangesAsync();

        return tarefaExistente;
    }

    public async Task<bool> DeleteTask(long id)
    {
        var tarefa = await context.Tarefas.FindAsync(id);
        if (tarefa != null)
        {
            context.Remove(tarefa);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            throw new Exception("Tarefa não encontrada.");
        }
    }

    public async Task<List<Tarefa>?> GetAllTasks()
    {
        var tarefasEncontradas = await context.Tarefas.ToListAsync();
        if (tarefasEncontradas == null || !tarefasEncontradas.Any())
        {
            return null;
        }
        return tarefasEncontradas;
    }

    public async Task<Tarefa?> GetTaskById(long id)
    {
        var tarefaEncontrada = await context.Tarefas.FindAsync(id);
        if (tarefaEncontrada == null)
        {
            return null;
        }
        return tarefaEncontrada;
    }
}