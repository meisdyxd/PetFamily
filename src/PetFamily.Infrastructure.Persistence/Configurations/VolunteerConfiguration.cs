using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Infrastructure.Persistence.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id).HasName("pk_volunteer");
        builder.Property(v => v.Id).HasColumnName("id");

        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name");

            fb.Property(p => p.Surname)
                .IsRequired()
                .HasColumnName("surname");

            fb.Property(p => p.Patronymic)
                .IsRequired(false)
                .HasColumnName("patronymic");
        });

        builder.ComplexProperty(v => v.Email, eb =>
        {
            eb.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("email");
        });

        builder.ComplexProperty(v => v.Description, db =>
        {
            db.Property(p => p.Value)
                .IsRequired(false)
                .HasColumnName("description");
        });

        builder.ComplexProperty(v => v.EmployeeExperience, eb =>
        {
            eb.Property(p => p.Year)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("years_employee_exp");
        });

        builder.ComplexProperty(v => v.TelephoneNumber, nb =>
        {
            nb.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("telephone_number");
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(v => v.SocialNetworks, sb =>
        {
            sb.ToJson("social_networks");

            sb.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Constants.SocialNetwork.MAX_NAME_LENGTH)
                .HasColumnName("name");

            sb.Property(p => p.Link)
                .IsRequired()
                .HasMaxLength(Constants.SocialNetwork.MAX_LINK_LENGTH)
                .HasColumnName("link");
        });

        builder.OwnsMany(v => v.Requisits, rb =>
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

        builder.Navigation(v => v.Pets).AutoInclude();
    }
}
