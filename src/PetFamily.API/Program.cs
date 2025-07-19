using PetFamily.API;
using PetFamily.API.Middlewares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configurations = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Seq(serverUrl: configurations.GetConnectionString("Seq")
        ?? throw new Exception("Не найдена строка подключения к 'Seq'"))
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug,
    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] - {SourceContext}: {Message:lj}{NewLine}")
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .CreateLogger();

services
    .AddLayers(configurations)
    .AddSwagger()
    .AddHandlers()
    .AddSerilog()
    .AddRepositories()
    .AddControllers();


var app = builder.Build();

await app.ApplyMigrations();

app.UseMiddleware<ExceptionMiddleware>();

app.UseConfigureSwagger();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
