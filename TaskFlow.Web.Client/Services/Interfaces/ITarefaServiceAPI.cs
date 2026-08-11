using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;

namespace TaskFlow.Web.Client.Services.Interfaces;

public interface ITarefaServiceAPI
{
    public Task<List<TaskResponse>> GetTarefasAsync();
    public Task<TaskResponse?> CreateTarefaAsync(CreateTaskRequest request);
    public Task<UpdateTaskResponse?> UpdateTarefaAsync(long id, UpdateTaskRequest request);
    public Task<RemoveTaskResponse?> DeleteTarefaAsync(long id);
    public Task<TaskResponse> UpdateTaskStatusAsync(long id, Status status);
}