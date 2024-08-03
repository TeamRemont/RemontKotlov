
using MediatR;
using Microsoft.EntityFrameworkCore;
using RemontKotlov.Persistance;
using System.Reflection;

namespace RemontKotlov
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(ops =>
            {
                ops.UseNpgsql(builder.Configuration.GetConnectionString("Db"));
            });

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
