using System.Net.Http;
using TaskFlow.Web.Client.Services.Storage;

namespace TaskFlow.Web.Client.Authentication;

public class AuthorizationHandler : DelegatingHandler
{
    private readonly IStorageService _storageService;

    public AuthorizationHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Obter o token do armazenamento local
        var token = await _storageService.GetItemAsync<string>("token");

        if (!string.IsNullOrEmpty(token))
        {
            // Adicionar o token ao cabeçalho de autorização
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}