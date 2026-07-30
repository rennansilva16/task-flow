using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;

namespace TaskFlow.Application.Services;

public interface ITarefaService
{
    public Task<List<TaskResponse>?> GetAllTasks();
    public Task<TaskResponse> CreateTarefa(CreateTaskRequest request);
    public Task<UpdateTaskResponse> UpdateTarefa(long id, UpdateTaskRequest request);
    public Task<RemoveTaskResponse> DeleteTarefa(long id);
    public Task<TaskResponse> UpdateTaskStatus(long id, Status status);
    public Task<Tarefa?> GetTaskById(long id);
}