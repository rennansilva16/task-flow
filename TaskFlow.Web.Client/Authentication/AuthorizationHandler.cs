using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using TaskFlow.Web.Client.Services.Storage;

namespace TaskFlow.Web.Client.Authentication;

public class AuthorizationHandler : DelegatingHandler
{
    private readonly IStorageService _storageService;
    private readonly CustomAuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigationManager;

    public AuthorizationHandler(IStorageService storageService, CustomAuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
    {
        _storageService = storageService;
        _authenticationStateProvider = authenticationStateProvider;
        _navigationManager = navigationManager;
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

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(token))
        {
            // Se a resposta for 401 Unauthorized, significa que o token expirou ou é inválido.
            // Remover o token do armazenamento local
            await _storageService.RemoveItemAsync("token");
            _authenticationStateProvider.MarkUserAsLoggedOut();
            _navigationManager.NavigateTo("/login");
        }

        return response;
    }
}