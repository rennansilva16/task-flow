using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;
using System.Net.Http.Json;
using TaskFlow.Web.Client.Services.Interfaces;
public class TarefaService : ITarefaServiceAPI
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/tarefa";

    public TarefaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TaskResponse>> GetTarefasAsync()
    {   
        var tarefasEncontradas = await _httpClient.GetFromJsonAsync<List<TaskResponse>>(BaseUrl);

        return tarefasEncontradas != null ? tarefasEncontradas : new List<TaskResponse>();
    }

    public async Task<TaskResponse?> CreateTarefaAsync(CreateTaskRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TaskResponse>();
        }
        return null;
    }

    public async Task<UpdateTaskResponse?> UpdateTarefaAsync(long id, UpdateTaskRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UpdateTaskResponse>();
        }
        return null;
    }

    public async Task<RemoveTaskResponse?> DeleteTarefaAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<RemoveTaskResponse>();
        }
        throw new Exception("Erro ao excluir tarefa.");
    }

    public async Task<TaskResponse> UpdateTaskStatusAsync(long id, Status status)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}/status", status);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TaskResponse>()
                ?? throw new Exception("Resposta inválida ao atualizar status da tarefa.");
        }
        throw new Exception("Erro ao atualizar status da tarefa.");
    }
}