using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.VolunteerManager;

namespace PetFamily.Infrastructure.Persistence.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

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
            sb.ToJson();

            sb.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("name");

            sb.Property(p => p.Link)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("link");
        });

        builder.OwnsMany(v => v.Requisits, rb =>
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
    }
}
