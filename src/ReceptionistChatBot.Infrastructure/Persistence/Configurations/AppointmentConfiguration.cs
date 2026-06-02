using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments");

        builder.HasKey(appointment => appointment.Id);

        builder.Property(appointment => appointment.Id).HasColumnName("id");

        builder.Property(appointment => appointment.PatientId)
            .HasColumnName("patient_id")
            .IsRequired();

        builder.Property(appointment => appointment.StaffMemberId)
            .HasColumnName("staff_member_id");

        builder.Property(appointment => appointment.DepartmentId)
            .HasColumnName("department_id");

        builder.Property(appointment => appointment.StartsAtUtc)
            .HasColumnName("starts_at_utc")
            .IsRequired();

        builder.Property(appointment => appointment.EndsAtUtc)
            .HasColumnName("ends_at_utc")
            .IsRequired();

        builder.Property(appointment => appointment.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(appointment => appointment.Reason)
            .HasColumnName("reason")
            .HasMaxLength(1000);

        builder.Property(appointment => appointment.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(appointment => appointment.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(appointment => appointment.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<StaffMember>()
            .WithMany()
            .HasForeignKey(appointment => appointment.StaffMemberId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(appointment => appointment.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(appointment => appointment.PatientId);
        builder.HasIndex(appointment => appointment.StaffMemberId);
        builder.HasIndex(appointment => appointment.DepartmentId);
        builder.HasIndex(appointment => appointment.StartsAtUtc);
        builder.HasIndex(appointment => new { appointment.PatientId, appointment.StartsAtUtc });
        builder.HasIndex(appointment => new { appointment.StaffMemberId, appointment.StartsAtUtc });
    }
}
