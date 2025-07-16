using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.SpeciesManagement;

namespace PetFamily.Infrastructure.Persistence.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnName("Name");

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_ids");
    }
}
