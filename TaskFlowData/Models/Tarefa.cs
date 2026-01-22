using WorksheetData.Enums;

namespace WorksheetData.Models;

public class Tarefa
{
    public long Id { get; set; }
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataPrazo { get; set; }
    public Importancia Importancia { get; set; } = Importancia.Baixa;
    public Status Status { get; set; } = Status.Pendentes;
    public required TipoDeTarefa TipoDeTarefa { get; set; }

}