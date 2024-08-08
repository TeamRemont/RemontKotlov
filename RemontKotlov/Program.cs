
using MediatR;
using Microsoft.EntityFrameworkCore;
using RemontKotlov.Entities;
using RemontKotlov.Middlewares;
using RemontKotlov.Persistance;
using RemontKotlov.Services.MetaDatas;
using RemontKotlov.Services.TelegramSender;
using Serilog;
using System.Reflection;
using Telegram.Bot;

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
            builder.Services.AddScoped<IMetadataService, MetadataService>();
            builder.Services.AddScoped<ITelegramSender, TelegramSender>();

            builder.Services.AddSingleton<TelegramBotClient>(provider =>
            {
                var botToken = $"7120169951:AAHSC0lVtBJhcePlU9V8Twxw4l7JSOW_ebs";
                return new TelegramBotClient(botToken);
            });

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
