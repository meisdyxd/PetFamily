using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Infrastructure.Persistence.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id).HasName("pk_pet");
        builder.Property(p => p.Id).HasColumnName("id");

        builder.Property(p => p.SequenceNumber)
            .IsRequired()
            .HasColumnName("sequence_number");

        builder.Property(p => p.Moniker)
            .HasMaxLength(Constants.Pet.MAX_MONIKER_LENGTH)
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
                v => (PetStatus)Enum.Parse(typeof(PetStatus), v))
            .HasColumnName("status");

        builder.OwnsMany(p => p.Requisits, rb =>
        {
            rb.ToJson("requisits");

            rb.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Constants.Requisit.MAX_NAME_LENGTH)
                .HasColumnName("name");

            rb.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.Requisit.MAX_DESCRIPTION_LENGTH)
                .HasColumnName("description");

            rb.Property(p => p.DetailInstruction)
                .IsRequired(false)
                .HasMaxLength(Constants.Requisit.MAX_DETAIL_INSTRUCTION_LENGTH)
                .HasColumnName("detailInstruction");
        });

        builder.OwnsMany(p => p.Photos, pb =>
        {
            pb.ToJson("photos");
            
            pb.Property(p => p.Filename)
                .HasColumnName("filename");
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(v => v.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired(true)
            .HasColumnName("is_deleted");

        builder.Property(v => v.DeletionDate)
            .HasDefaultValue(null)
            .IsRequired(false)
            .HasColumnName("deletion_date");

        builder.Property(v => v.DeleteByCascade)
            .HasDefaultValue(false)
            .IsRequired(true)
            .HasColumnName("delete_by_cascade");
    }
}
