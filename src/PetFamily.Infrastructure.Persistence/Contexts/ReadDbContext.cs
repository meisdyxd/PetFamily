using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Contracts.VolunteerContracts.DTOs;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Infrastructure.Persistence.Contexts;

public class ReadDbContext: DbContext, IReadDbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options)
       : base(options)
    {
    }

    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
