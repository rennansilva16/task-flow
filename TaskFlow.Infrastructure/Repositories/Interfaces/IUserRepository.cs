namespace TaskFlow.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<Usuario> CreateUserAsync(Usuario usuario);
    public Task<Usuario?> GetUserByLoginAsync(string login);
}