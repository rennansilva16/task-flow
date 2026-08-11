using TaskFlow.Application.Identity;
using TaskFlow.Application.Persistence;
using TaskFlow.Application.Services;
using TaskFlow.Shared.Requests;
using Xunit;

namespace TaskFlow.Application.Tests;

public class TarefaServiceTests
{
    private const long UsuarioId = 42;

    [Fact]
    public async Task GetAllTasks_DeveRetornarSomenteTarefasObtidasParaOUsuarioAtual()
    {
        var repository = new FakeTarefaRepository
        {
            Tarefas =
            [
                CriarTarefa(1, "Primeira tarefa"),
                CriarTarefa(2, "Segunda tarefa", Status.Fazendo)
            ]
        };
        var service = CriarService(repository);

        var resultado = await service.GetAllTasks();

        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaListagem);
        Assert.Collection(resultado!,
            tarefa => Assert.Equal("Primeira tarefa", tarefa.Titulo),
            tarefa => Assert.Equal(Status.Fazendo, tarefa.Status));
    }

    [Fact]
    public async Task CreateTarefa_DeveVincularTarefaAoUsuarioAtual()
    {
        var repository = new FakeTarefaRepository { ProximoId = 7 };
        var service = CriarService(repository);
        var request = new CreateTaskRequest
        {
            Titulo = "Estudar testes",
            Descricao = "Cobrir regras de autorizacao",
            DataPrazo = new DateOnly(2026, 8, 10)
        };

        var resultado = await service.CreateTarefa(request);

        Assert.NotNull(repository.TarefaCriada);
        Assert.Equal(UsuarioId, repository.TarefaCriada!.UsuarioId);
        Assert.Equal(request.Titulo, resultado.Titulo);
        Assert.Equal(7, resultado.Id);
    }

    [Fact]
    public async Task UpdateTarefa_DeveAtualizarUsandoOUsuarioAtual()
    {
        var repository = new FakeTarefaRepository { TarefaParaAtualizar = CriarTarefa(8, "Titulo atualizado", Status.Finalizada) };
        var service = CriarService(repository);
        var request = new UpdateTaskRequest
        {
            Titulo = "Titulo atualizado",
            Descricao = "Nova descricao",
            DataPrazo = new DateOnly(2026, 8, 15),
            Status = Status.Finalizada
        };

        var resultado = await service.UpdateTarefa(8, request);

        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaAtualizacao);
        Assert.Equal(8, repository.TarefaAtualizada!.Id);
        Assert.Equal(Status.Finalizada, resultado.Status);
        Assert.Equal("Titulo atualizado", resultado.Title);
    }

    [Fact]
    public async Task UpdateTarefa_QuandoNaoPertenceAoUsuarioAtual_DeveLancarExcecao()
    {
        var repository = new FakeTarefaRepository();
        var service = CriarService(repository);
        var request = new UpdateTaskRequest { Titulo = "Sem acesso" };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateTarefa(99, request));
        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaAtualizacao);
    }

    [Fact]
    public async Task DeleteTarefa_DeveExcluirSomenteComOUsuarioAtual()
    {
        var repository = new FakeTarefaRepository { ResultadoDaExclusao = true };
        var service = CriarService(repository);

        var resultado = await service.DeleteTarefa(11);

        Assert.True(resultado.Excluido);
        Assert.Equal(11, repository.UltimoIdExcluido);
        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaExclusao);
    }

    [Fact]
    public async Task UpdateTaskStatus_DeveAlterarStatusDeTarefaDoUsuarioAtual()
    {
        var tarefa = CriarTarefa(12, "Em andamento");
        var repository = new FakeTarefaRepository { TarefaPorId = tarefa, TarefaParaAtualizar = tarefa };
        var service = CriarService(repository);

        var resultado = await service.UpdateTaskStatus(12, Status.Finalizada);

        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaBusca);
        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaAtualizacao);
        Assert.Equal(Status.Finalizada, repository.TarefaAtualizada!.Status);
        Assert.Equal(Status.Finalizada, resultado.Status);
    }

    [Fact]
    public async Task GetTaskById_QuandoTarefaNaoPertenceAoUsuarioAtual_DeveRetornarNulo()
    {
        var repository = new FakeTarefaRepository { TarefaPorId = null };
        var service = CriarService(repository);

        var resultado = await service.GetTaskById(13);

        Assert.Null(resultado);
        Assert.Equal(13, repository.UltimoIdBuscado);
        Assert.Equal(UsuarioId, repository.UltimoUsuarioIdDaBusca);
    }

    private static TarefaService CriarService(FakeTarefaRepository repository) =>
        new(repository, new FakeCurrentUser(UsuarioId));

    private static Tarefa CriarTarefa(long id, string titulo, Status status = Status.Pendente) => new()
    {
        Id = id,
        Titulo = titulo,
        Status = status,
        UsuarioId = UsuarioId
    };

    private sealed class FakeCurrentUser(long id) : ICurrentUser
    {
        public long Id => id;
        public string? Name => "Usuario de teste";
        public string Login => "usuario.teste";
    }

    private sealed class FakeTarefaRepository : ITarefaRepository
    {
        public List<Tarefa> Tarefas { get; set; } = [];
        public Tarefa? TarefaPorId { get; set; }
        public Tarefa? TarefaParaAtualizar { get; set; }
        public Tarefa? TarefaCriada { get; private set; }
        public Tarefa? TarefaAtualizada { get; private set; }
        public bool ResultadoDaExclusao { get; set; }
        public long ProximoId { get; set; } = 1;
        public long? UltimoUsuarioIdDaListagem { get; private set; }
        public long? UltimoUsuarioIdDaAtualizacao { get; private set; }
        public long? UltimoUsuarioIdDaBusca { get; private set; }
        public long? UltimoUsuarioIdDaExclusao { get; private set; }
        public long? UltimoIdExcluido { get; private set; }
        public long? UltimoIdBuscado { get; private set; }

        public Task<Tarefa> CreateTask(Tarefa tarefa)
        {
            tarefa.Id = ProximoId;
            TarefaCriada = tarefa;
            return Task.FromResult(tarefa);
        }

        public Task<Tarefa?> UpdateTask(Tarefa tarefa, long usuarioId)
        {
            UltimoUsuarioIdDaAtualizacao = usuarioId;
            if (TarefaParaAtualizar == null)
                return Task.FromResult<Tarefa?>(null);

            TarefaAtualizada = tarefa;
            return Task.FromResult<Tarefa?>(tarefa);
        }

        public Task<bool> DeleteTask(long id, long usuarioId)
        {
            UltimoIdExcluido = id;
            UltimoUsuarioIdDaExclusao = usuarioId;
            return Task.FromResult(ResultadoDaExclusao);
        }

        public Task<List<Tarefa>> GetAllTasks(long usuarioId)
        {
            UltimoUsuarioIdDaListagem = usuarioId;
            return Task.FromResult(Tarefas);
        }

        public Task<Tarefa?> GetTaskById(long id, long usuarioId)
        {
            UltimoIdBuscado = id;
            UltimoUsuarioIdDaBusca = usuarioId;
            return Task.FromResult(TarefaPorId);
        }
    }
}
