using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class CompanyInformationConfiguration : IEntityTypeConfiguration<CompanyInformation>
{
    public void Configure(EntityTypeBuilder<CompanyInformation> builder)
    {
        builder.ToTable("company_information");

        builder.HasKey(company => company.Id);

        builder.Property(company => company.Id)
            .HasColumnName("id");

        builder.Property(company => company.CompanyName)
            .HasColumnName("company_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(company => company.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);

        builder.Property(company => company.Address)
            .HasColumnName("address")
            .HasMaxLength(500);

        builder.Property(company => company.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(30);

        builder.Property(company => company.Email)
            .HasColumnName("email")
            .HasMaxLength(256);

        builder.Property(company => company.WebsiteUrl)
            .HasColumnName("website_url")
            .HasMaxLength(500);

        builder.Property(company => company.BusinessHours)
            .HasColumnName("business_hours")
            .HasMaxLength(1000);

        builder.Property(company => company.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(company => company.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(company => company.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(company => company.IsActive);
        builder.HasIndex(company => company.CompanyName);
    }
}
