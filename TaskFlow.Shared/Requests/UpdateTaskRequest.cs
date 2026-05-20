namespace TaskFlow.Shared.Requests;

public class UpdateTaskRequest
{
  public long Id { get; set;}
  public string Titulo { get; set; } = null!;
  public string? Descricao { get; set; }
  public DateOnly? DataPrazo { get; set; }
  public Status Status { get; set; }
}