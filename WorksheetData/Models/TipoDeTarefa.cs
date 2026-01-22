namespace WorksheetData.Models;

public class TipoDeTarefa
{
    public long Id { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
}
