using Microsoft.AspNetCore.Mvc;
using TaskFlow.Shared.Requests;
using TaskFlow.Shared.Responses;
namespace TaskFlowAPI.Controllers
{
    [Route("api/[controller]")]
    public class TarefaController : Controller
    {
        private readonly ILogger<TarefaController> _logger;
        private readonly TaskFlowDbContext _context;
        private readonly TarefaService tarefaService;

        public TarefaController(ILogger<TarefaController> logger, TaskFlowDbContext context, TarefaService tarefaService)
        {
            _logger = logger;
            _context = context;
            this.tarefaService = tarefaService;
        }

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
}