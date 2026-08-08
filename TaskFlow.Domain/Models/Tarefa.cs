using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

public class Tarefa
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Titulo { get; set; } = null!;
    [MaxLength(500)]
    public string? Descricao { get; set; }
    public DateOnly? DataPrazo { get; set; }
    public Status Status { get; set; } = Status.Pendente;
    public long UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
