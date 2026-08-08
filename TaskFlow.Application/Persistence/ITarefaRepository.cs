namespace TaskFlow.Application.Persistence;

public interface ITarefaRepository
{
    public Task<Tarefa> CreateTask(Tarefa tarefa);
    public Task<Tarefa?> UpdateTask(Tarefa tarefa, long usuarioId);
    public Task<bool> DeleteTask(long id, long usuarioId);
    public Task<List<Tarefa>> GetAllTasks(long usuarioId);
    public Task<Tarefa?> GetTaskById(long id, long usuarioId);
}
