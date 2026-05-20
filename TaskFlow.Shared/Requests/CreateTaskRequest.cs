
namespace TaskFlow.Shared.Requests;

public class CreateTaskRequest
{
    public string Titulo { get; set; } = null!;
    public string? Descricao { get; set; }
    public DateOnly? DataPrazo { get; set; }
}