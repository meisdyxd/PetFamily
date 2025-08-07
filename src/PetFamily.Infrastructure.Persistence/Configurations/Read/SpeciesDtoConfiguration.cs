using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.SpeciesModule.Models;

namespace PetFamily.Infrastructure.Persistence.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id).HasName("pk_species");
        builder.Property(s => s.Id).HasColumnName("id");
        
        builder.Property(s => s.Name).HasColumnName("name");
    }
}
