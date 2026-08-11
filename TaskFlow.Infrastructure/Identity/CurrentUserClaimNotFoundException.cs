namespace TaskFlow.Infrastructure.Identity;

public class CurrentUserClaimNotFoundException : Exception
{
     public CurrentUserClaimNotFoundException(string claimType)
    : base($"A claim '{claimType}' não foi encontrada no usuário autenticado.")
{
}
}