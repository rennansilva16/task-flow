namespace TaskFlow.Application.Identity;

public interface ICurrentUser
{
    long Id { get;}
    string? Name { get; }
    string Login { get; }
}