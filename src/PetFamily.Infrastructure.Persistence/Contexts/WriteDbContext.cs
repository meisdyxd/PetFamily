using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Infrastructure.Persistence.Contexts;

public class WriteDbContext: DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options)
       : base(options)
    {
    }

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Species> Species => Set<Species>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
