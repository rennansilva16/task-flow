using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskFlow.Web.Client;
using TaskFlow.Web.Client.Authentication;
using TaskFlow.Web.Client.Services.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using TaskFlow.Web.Client.Services.Interfaces;
using TaskFlow.Web.Client.Authentication.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();

builder.Services.AddHttpClient("TaskFlowAPI",client =>
{
    client.BaseAddress = new Uri(builder.Configuration["TaskFlowAPI:BaseUrl"] ?? throw new InvalidOperationException("Base URL da API não configurada."));
}).AddHttpMessageHandler<AuthorizationHandler>();

builder.Services.AddScoped<HttpClient>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("TaskFlowAPI");
});
    
builder.Services.AddScoped<ITarefaServiceAPI, TarefaService>();
builder.Services.AddScoped<IUserServiceAPI, UserServiceAPI>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<AuthorizationHandler>();

builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IJwtParser, JwtParser>();


await builder.Build().RunAsync();
