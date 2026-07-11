using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskFlow.Web.Client;
using TaskFlow.Web.Client.Authentication;
using TaskFlow.Web.Client.Services.Storage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["TaskFlowAPI:BaseUrl"] ?? throw new InvalidOperationException("Base URL da API não configurada.")) });

builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<UserServiceAPI>();
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<IStorageService, StorageService>();


await builder.Build().RunAsync();
