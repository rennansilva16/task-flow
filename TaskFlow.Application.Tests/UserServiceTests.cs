using TaskFlow.Application.Identity;
using TaskFlow.Application.Persistence;
using TaskFlow.Application.Services;
using TaskFlow.Shared.Requests;
using Xunit;

namespace TaskFlow.Application.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task CreateUserAsync_DevePersistirSomenteHashDaSenha()
    {
        var repository = new FakeUserRepository();
        var service = CreateService(repository);
        var request = new CreateUserRequest { Nome = "Ana", Login = "ana", Senha = "SenhaSegura123" };

        await service.CreateUserAsync(request);

        Assert.NotNull(repository.CreatedUser);
        Assert.NotEqual(request.Senha, repository.CreatedUser!.Senha);
        Assert.StartsWith("PBKDF2-SHA512$", repository.CreatedUser.Senha);
        Assert.True(new Pbkdf2PasswordHasher().Verify(repository.CreatedUser.Senha, request.Senha));
    }

    [Fact]
    public async Task LoginAsync_ComSenhaHashCorreta_DeveRetornarToken()
    {
        var hasher = new Pbkdf2PasswordHasher();
        var repository = new FakeUserRepository
        {
            UserByLogin = new Usuario { Id = 7, Nome = "Ana", Login = "ana", Senha = hasher.Hash("SenhaSegura123") }
        };
        var service = CreateService(repository, hasher);

        var result = await service.LoginAsync(new LoginRequest { Login = "ana", Password = "SenhaSegura123" });

        Assert.NotNull(result);
        Assert.Equal("token-de-teste", result.Token);
    }

    [Fact]
    public async Task LoginAsync_ComSenhaIncorreta_DeveFalhar()
    {
        var hasher = new Pbkdf2PasswordHasher();
        var repository = new FakeUserRepository
        {
            UserByLogin = new Usuario { Id = 7, Nome = "Ana", Login = "ana", Senha = hasher.Hash("SenhaSegura123") }
        };
        var service = CreateService(repository, hasher);

        var result = await service.LoginAsync(new LoginRequest { Login = "ana", Password = "senha-errada" });

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_ComSenhaLegadaEmTextoPuro_DeveContinuarFuncionando()
    {
        var repository = new FakeUserRepository
        {
            UserByLogin = new Usuario { Id = 7, Nome = "Ana", Login = "ana", Senha = "senha-legada" }
        };
        var service = CreateService(repository);

        var result = await service.LoginAsync(new LoginRequest { Login = "ana", Password = "senha-legada" });

        Assert.NotNull(result);
    }

    private static UserService CreateService(FakeUserRepository repository, IPasswordHasher? hasher = null) =>
        new(repository, new FakeJwtService(), hasher ?? new Pbkdf2PasswordHasher());

    private sealed class FakeJwtService : IJwtService
    {
        public string GenerateToken(Usuario usuario) => "token-de-teste";
    }

    private sealed class FakeUserRepository : IUserRepository
    {
        public Usuario? CreatedUser { get; private set; }
        public Usuario? UserByLogin { get; set; }

        public Task<Usuario> CreateUserAsync(Usuario usuario)
        {
            usuario.Id = 1;
            CreatedUser = usuario;
            return Task.FromResult(usuario);
        }

        public Task<Usuario?> GetUserByLoginAsync(string login) => Task.FromResult(UserByLogin);
    }
}
