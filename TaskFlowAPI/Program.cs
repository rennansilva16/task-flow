
using System;
using Microsoft.EntityFrameworkCore;

namespace WorksheetAPI
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

            builder.Services.AddDbContext<WorksheetContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WorksheetConnection")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WorksheetContext>();
                if (dbContext.Database.CanConnect())
                {
                    Console.WriteLine("Conexão com o banco de dados funcionando!");
                }
                else
                {
                    Console.WriteLine("Não foi possível conectar ao banco de dados.");
                }
            }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
