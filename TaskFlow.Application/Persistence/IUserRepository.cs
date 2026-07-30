namespace TaskFlow.Application.Persistence;

public interface IUserRepository
{
    public Task<Usuario> CreateUserAsync(Usuario usuario);
    public Task<Usuario?> GetUserByLoginAsync(string login);
}