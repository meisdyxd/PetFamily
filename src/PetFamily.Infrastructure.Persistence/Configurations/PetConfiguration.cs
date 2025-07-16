using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManager;

namespace PetFamily.Infrastructure.Persistence.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Moniker)
            .HasMaxLength(Constants.MAX_MONIKER_LENGTH)
            .IsRequired()
            .HasColumnName("moniker");

        builder.ComplexProperty(p => p.Species, sb =>
        {
            sb.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("species_id");
        });

        builder.ComplexProperty(v => v.Description, db =>
        {
            db.Property(p => p.Value)
                .IsRequired(false)
                .HasColumnName("description");
        });

        builder.ComplexProperty(p => p.Breed, sb =>
        {
            sb.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("breed_id");
        });

        builder.Property(p => p.Coloration)
            .IsRequired()
            .HasColumnName("coloration");

        builder.Property(p => p.HealthInfo)
            .IsRequired()
            .HasColumnName("health_info");

        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(p => p.Country)
                .IsRequired()
                .HasColumnName("country");

            ab.Property(p => p.Region)
                .IsRequired()
                .HasColumnName("region");

            ab.Property(p => p.City)
                .IsRequired()
                .HasColumnName("city");

            ab.Property(p => p.Street)
                .IsRequired()
                .HasColumnName("street");

            ab.Property(p => p.Flat)
                .IsRequired()
                .HasColumnName("flat");
        });

        builder.Property(p => p.Weight)
            .IsRequired()
            .HasColumnName("weight");

        builder.Property(p => p.Height)
            .IsRequired()
            .HasColumnName("height");

        builder.ComplexProperty(p => p.OwnerTelephoneNumber, nb =>
        {
            nb.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("telephone_number");
        });

        builder.Property(p => p.IsCastrated)
            .IsRequired()
            .HasColumnName("is_castrated");

        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnName("birth_date");

        builder.Property(p => p.IsVaccinated)
            .IsRequired()
            .HasColumnName("is_vaccinated");

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (PetStatus)Enum.Parse(typeof(PetStatus), v));

        builder.OwnsMany(p => p.Requisits, rb =>
        {
            rb.ToJson();

            rb.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");

            rb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("description");

            rb.Property(p => p.DetailInstruction)
                .IsRequired(false)
                .HasMaxLength(600)
                .HasColumnName("detailInstruction");
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
    }
}
