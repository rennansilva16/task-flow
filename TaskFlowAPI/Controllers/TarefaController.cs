using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;
using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;
namespace TaskFlowAPI.Controllers;

[Route("api/[controller]")]
public class TarefaController : Controller
{
    private readonly ITarefaService tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        this.tarefaService = tarefaService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tarefas = await tarefaService.GetAllTasks();
        return Ok(tarefas);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTarefa([FromBody] CreateTaskRequest request)
    {
        TaskResponse tarefaCriada = await tarefaService.CreateTarefa(request);
        return StatusCode(201, tarefaCriada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTarefa(long id, [FromBody] UpdateTaskRequest request)
    {
        // Lógica para atualizar uma tarefa existente
        UpdateTaskResponse tarefa = await tarefaService.UpdateTarefa(id, request);
        return Ok(tarefa);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarefa(long id)
    {
        RemoveTaskResponse response = await tarefaService.DeleteTarefa(id);
        if (!response.Excluido) return NotFound("Tarefa não encontrada.");
        return Ok(response);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(long id, [FromBody] Status status)
    {
        var tarefa = await tarefaService.UpdateTaskStatus(id, status);
        return Ok(tarefa);
    }
}