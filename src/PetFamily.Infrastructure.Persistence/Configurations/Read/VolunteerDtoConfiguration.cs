using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Infrastructure.Persistence.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id).HasName("pk_volunteer");
        builder.Property(v => v.Id).HasColumnName("id");
        
        builder.Property(v => v.Name).HasColumnName("name");
        builder.Property(v => v.Surname).HasColumnName("surname");
        builder.Property(v => v.Patronymic).HasColumnName("patronymic");
        builder.Property(v => v.Description).HasColumnName("description");
        builder.Property(v => v.Email).HasColumnName("email");
        builder.Property(v => v.EmployeeExperience).HasColumnName("years_employee_exp");
        builder.Property(v => v.TelephoneNumber).HasColumnName("telephone_number");

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
            rb.ToJson("requisites");

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
    }
}
