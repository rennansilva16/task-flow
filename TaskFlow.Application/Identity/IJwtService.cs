namespace TaskFlow.Application.Identity;

public interface IJwtService
{
     string GenerateToken(Usuario usuario);
}