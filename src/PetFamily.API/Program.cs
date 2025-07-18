using PetFamily.API;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configurations = builder.Configuration;

services
    .AddLayers(configurations)
    .AddSwagger()
    .AddHandlers()
    .AddRepositories()
    .AddControllers();


var app = builder.Build();

app.UseConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
