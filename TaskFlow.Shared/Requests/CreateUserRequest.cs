using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string Login { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Senha { get; set; } = null!;
}