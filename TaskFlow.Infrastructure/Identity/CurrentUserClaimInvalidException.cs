namespace TaskFlow.Infrastructure.Identity;

public class CurrentUserClaimInvalidException : Exception
{
     public CurrentUserClaimInvalidException(string claimType, string value)
        : base($"A claim '{claimType}' possui um valor inválido: '{value}'.")
    {
    }
}