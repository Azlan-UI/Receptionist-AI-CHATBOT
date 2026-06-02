using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(department => department.Id);

        builder.Property(department => department.Id).HasColumnName("id");

        builder.Property(department => department.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(department => department.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(department => department.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(department => department.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(department => department.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(department => department.Name)
            .IsUnique();

        builder.HasIndex(department => department.IsActive);
    }
}
