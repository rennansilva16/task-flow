using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TaskFlowData.Models;

public class Tarefa
{
    [Key]
    public long Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descricao { get; set; }
    public DateOnly? DataPrazo { get; set; }
    public Status Status { get; set; } = Status.Pendente;
}