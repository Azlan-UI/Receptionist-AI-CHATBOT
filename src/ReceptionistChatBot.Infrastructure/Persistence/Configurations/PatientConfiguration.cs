using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");

        builder.HasKey(patient => patient.Id);

        builder.Property(patient => patient.Id).HasColumnName("id");

        builder.Property(patient => patient.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(patient => patient.Email)
            .HasColumnName("email")
            .HasMaxLength(256);

        builder.Property(patient => patient.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(patient => patient.DateOfBirth)
            .HasColumnName("date_of_birth");

        builder.Property(patient => patient.Notes)
            .HasColumnName("notes")
            .HasMaxLength(1000);

        builder.Property(patient => patient.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(patient => patient.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(patient => patient.PhoneNumber);
        builder.HasIndex(patient => patient.Email)
            .HasFilter("email IS NOT NULL");
        builder.HasIndex(patient => patient.CreatedAtUtc);
    }
}
