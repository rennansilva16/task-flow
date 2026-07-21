
using System;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Authentication;
using TaskFlow.Application.Services;
using TaskFlow.Infrastructure.Repositories;
using TaskFlowAPI.Configurations;

namespace TaskFlowAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<TarefaRepository>();
            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddScoped<TarefaService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IJwtService, JwtService>();


            builder.Services.AddDbContext<TaskFlowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TaskFlowConnection")));

            builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7206",
        "http://localhost:7206",
         "http://192.168.0.12:7206")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

            builder.Services.AddJwtAuthentication(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TaskFlowDbContext>();
                    if (dbContext.Database.CanConnect())
                    {
                        Console.WriteLine("Conex�o com o banco de dados funcionando!");

                    }
                    else
                    {
                        Console.WriteLine("N�o foi poss�vel conectar ao banco de dados.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao tentar conectar ao banco de dados: {ex.Message}");
                }

            }

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            // Configurar para abrir no swagger

            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.UseHttpsRedirection();

            app.UseCors("AllowBlazor");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
