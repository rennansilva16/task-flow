namespace WorksheetData.Models;

public class Usuario
{
    public long Id { get; set; }
    public required string Nome { get; set; }
    public required string Login { get; set; }
    public required string Senha { get; set; }
    public List<Tarefa>? Tarefas { get; set; }
}
