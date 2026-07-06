namespace TaskFlow.Application.Authentication;

public interface IJwtService
{
     string GenerateToken(Usuario usuario);
}