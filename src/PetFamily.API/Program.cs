using PetFamily.API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configurations = builder.Configuration;

var serilogConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(serilogConfiguration)
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

app.ConfigureMiddlewares();

app.MapControllers();

app.Run();
