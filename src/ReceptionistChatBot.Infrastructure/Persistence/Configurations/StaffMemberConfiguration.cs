using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class StaffMemberConfiguration : IEntityTypeConfiguration<StaffMember>
{
    public void Configure(EntityTypeBuilder<StaffMember> builder)
    {
        builder.ToTable("staff_members");

        builder.HasKey(staffMember => staffMember.Id);

        builder.Property(staffMember => staffMember.Id).HasColumnName("id");

        builder.Property(staffMember => staffMember.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(staffMember => staffMember.RoleTitle)
            .HasColumnName("role_title")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(staffMember => staffMember.Email)
            .HasColumnName("email")
            .HasMaxLength(256);

        builder.Property(staffMember => staffMember.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired();

        builder.Property(staffMember => staffMember.IsAvailableForAppointments)
            .HasColumnName("is_available_for_appointments")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(staffMember => staffMember.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(staffMember => staffMember.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(staffMember => staffMember.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(staffMember => staffMember.DepartmentId);
        builder.HasIndex(staffMember => staffMember.Email)
            .HasFilter("email IS NOT NULL");
        builder.HasIndex(staffMember => new { staffMember.DepartmentId, staffMember.IsAvailableForAppointments });
    }
}
