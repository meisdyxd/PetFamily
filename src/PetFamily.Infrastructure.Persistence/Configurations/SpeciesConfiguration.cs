using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.SpeciesManagement;

namespace PetFamily.Infrastructure.Persistence.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id).HasName("pk_species");
        builder.Property(s => s.Id).HasColumnName("id");

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_ids");
    }
}
