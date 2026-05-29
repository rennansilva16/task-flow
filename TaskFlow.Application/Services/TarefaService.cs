using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;
using TaskFlowData.Models;

public class TarefaService
{
    private readonly TarefaRepository tarefaRepository;

    public TarefaService(TarefaRepository tarefaRepository)
    {
        this.tarefaRepository = tarefaRepository;
    }

    public async Task<List<TaskResponse>?> GetAllTasks()
    {
        List<Tarefa>? tarefasEncontradas = await tarefaRepository.GetAllTasks();
        List<TaskResponse>? tarefasResponse = null;
        if (tarefasEncontradas != null)
        {
            tarefasResponse = tarefasEncontradas.Select(t => new TaskResponse()
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataPrazo = t.DataPrazo.HasValue ? t.DataPrazo.Value : null,
                Status = t.Status
            }).ToList();
        }
        else
        {
            return null;
        }
        return tarefasResponse;
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
            DataPrazo = request.DataPrazo
        };
        var tarefaCriada = await tarefaRepository.CreateTask(tarefa);

        return tarefaCriada != null ? new TaskResponse()
        {
            Id = tarefaCriada.Id,
            Titulo = tarefaCriada.Titulo,
            Descricao = tarefaCriada.Descricao,
            DataPrazo = tarefaCriada.DataPrazo,
            Status = tarefaCriada.Status
        } : throw new Exception("Erro ao criar tarefa");
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

        var tarefaAtualizada = await tarefaRepository.UpdateTask(tarefa);
        if (tarefaAtualizada == null) throw new Exception("Erro ao atualizar tarefa");

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
        var deletada = await tarefaRepository.DeleteTask(id);
        return new RemoveTaskResponse()
        {
            Id = id,
            Excluido = deletada
        };
    }

    public async Task<TaskResponse> UpdateTaskStatus(long id, Status status)
    {
        // Lógica para atualizar o status de uma tarefa
        // Exemplo: tarefaRepository.UpdateTaskStatus(id, status);
        var tarefaExistente = await tarefaRepository.GetTaskById(id);
        if (tarefaExistente == null)
            throw new Exception("Tarefa não encontrada.");
        tarefaExistente.Status = status;
        await tarefaRepository.UpdateTask(tarefaExistente);
        return new TaskResponse()
        {
            Id = tarefaExistente.Id,
            Titulo = tarefaExistente.Titulo,
            Descricao = tarefaExistente.Descricao,
            DataPrazo = tarefaExistente.DataPrazo,
            Status = tarefaExistente.Status
        };
    }

    public async Task<Tarefa?> GetTaskById(long id)
    {
        // Lógica para obter uma tarefa por ID
        // Exemplo: return tarefaRepository.GetTaskById(id);
        var tarefaEncontrada = await tarefaRepository.GetTaskById(id);
        if (tarefaEncontrada == null)
        {
            return null;
        }
        return tarefaEncontrada;
    }
}