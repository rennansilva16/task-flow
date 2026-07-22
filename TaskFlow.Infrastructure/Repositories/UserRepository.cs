using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure.Repositories.Interfaces;

namespace TaskFlow.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskFlowDbContext _context;

    public UserRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> CreateUserAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> GetUserByLoginAsync(string login)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
    }
}