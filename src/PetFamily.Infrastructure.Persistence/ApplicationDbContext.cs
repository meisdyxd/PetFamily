using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.VolunteerManager;

namespace PetFamily.Infrastructure.Persistence;

public class ApplicationDbContext: DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Species> Species => Set<Species>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("Database");
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseLoggerFactory(CreateILoggerFactory());
    }

    private ILoggerFactory CreateILoggerFactory() =>
        LoggerFactory.Create(b => { b.AddConsole(); });
}
