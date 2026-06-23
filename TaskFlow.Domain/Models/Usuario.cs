using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nome { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Login { get; set; } = null!;
    [Required]
    [MaxLength(255)]
    public string Senha { get; set; } = null!;
    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}