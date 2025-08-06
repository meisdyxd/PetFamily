using PetFamily.API;
using PetFamily.API.Registrations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configurations = builder.Configuration;

SerilogRegistration.Execute(configurations);

services
    .AddLayers(configurations)
    .AddSwagger()
    .AddSerilog()
    .AddRepositories()
    .AddControllers();


var app = builder.Build();

await app.ApplyMigrations();

app.ConfigureMiddlewares();

app.MapControllers();

app.Run();
