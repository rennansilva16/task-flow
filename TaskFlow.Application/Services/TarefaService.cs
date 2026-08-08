using TaskFlow.Application.Identity;
using TaskFlow.Application.Persistence;
using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;
namespace TaskFlow.Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository tarefaRepository;
    private readonly ICurrentUser currentUser;

    public TarefaService(ITarefaRepository tarefaRepository, ICurrentUser currentUser)
    {
        this.tarefaRepository = tarefaRepository;
        this.currentUser = currentUser;
    }

    public async Task<List<TaskResponse>?> GetAllTasks()
    {
        var tarefasEncontradas = await tarefaRepository.GetAllTasks(currentUser.Id);

        return tarefasEncontradas.Select(ToTaskResponse).ToList();
    }
    public async Task<TaskResponse> CreateTarefa(CreateTaskRequest request)
    {
        if (request == null)
        {
            throw new Exception("Erro ao criar tarefa");
        }
        Tarefa tarefa = new()
        {
            Titulo = request.Titulo,
            Descricao = request.Descricao,
            DataPrazo = request.DataPrazo,
            UsuarioId = currentUser.Id
        };
        var tarefaCriada = await tarefaRepository.CreateTask(tarefa);

        return tarefaCriada != null ? ToTaskResponse(tarefaCriada) : throw new Exception("Erro ao criar tarefa");
    }

    public async Task<UpdateTaskResponse> UpdateTarefa(long id, UpdateTaskRequest request)
    {
        if (request == null)
        {
            throw new Exception("Erro ao atualizar tarefa");
        }
        // Lógica para atualizar uma tarefa existente
        Tarefa tarefa = new()
        {
            Id = id,
            Titulo = request.Titulo,
            Descricao = request.Descricao,
            DataPrazo = request.DataPrazo,
            Status = request.Status
        };

        var tarefaAtualizada = await tarefaRepository.UpdateTask(tarefa, currentUser.Id);
        if (tarefaAtualizada == null) throw new KeyNotFoundException("Tarefa não encontrada.");

        var response = new UpdateTaskResponse()
        {
            Id = tarefaAtualizada.Id,
            Title = tarefaAtualizada.Titulo,
            Description = tarefaAtualizada.Descricao,
            DueDate = tarefaAtualizada.DataPrazo,
            Status = tarefaAtualizada.Status
        };
        return response;
    }

    public async Task<RemoveTaskResponse> DeleteTarefa(long id)
    {
        var deletada = await tarefaRepository.DeleteTask(id, currentUser.Id);
        return new RemoveTaskResponse()
        {
            Id = id,
            Excluido = deletada
        };
    }

    public async Task<TaskResponse> UpdateTaskStatus(long id, Status status)
    {
        var tarefaExistente = await tarefaRepository.GetTaskById(id, currentUser.Id);
        if (tarefaExistente == null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        tarefaExistente.Status = status;
        var tarefaAtualizada = await tarefaRepository.UpdateTask(tarefaExistente, currentUser.Id);
        if (tarefaAtualizada == null) throw new KeyNotFoundException("Tarefa não encontrada.");

        return ToTaskResponse(tarefaAtualizada);
    }

    public async Task<Tarefa?> GetTaskById(long id)
    {
        return await tarefaRepository.GetTaskById(id, currentUser.Id);
    }

    private static TaskResponse ToTaskResponse(Tarefa tarefa) => new()
    {
        Id = tarefa.Id,
        Titulo = tarefa.Titulo,
        Descricao = tarefa.Descricao,
        DataPrazo = tarefa.DataPrazo,
        Status = tarefa.Status
    };
}
