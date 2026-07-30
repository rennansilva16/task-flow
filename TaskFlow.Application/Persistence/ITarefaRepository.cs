namespace TaskFlow.Application.Persistence;

public interface ITarefaRepository
{
    public Task<Tarefa> CreateTask(Tarefa tarefa);
    public Task<Tarefa> UpdateTask(Tarefa tarefa);
    public Task<bool> DeleteTask(long id);
    public Task<List<Tarefa>?> GetAllTasks();
    public Task<Tarefa?> GetTaskById(long id);
}