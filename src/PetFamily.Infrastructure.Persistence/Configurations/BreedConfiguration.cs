using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.SpeciesManagement;

namespace PetFamily.Infrastructure.Persistence.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id).HasName("pk_breed");
        builder.Property(b => b.Id).HasColumnName("id");

        builder.Property(b => b.Name)
            .IsRequired()
            .HasColumnName("name");
    }
}
