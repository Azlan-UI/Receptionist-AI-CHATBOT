using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class FaqConfiguration : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.ToTable("faqs");

        builder.HasKey(faq => faq.Id);

        builder.Property(faq => faq.Id)
            .HasColumnName("id");

        builder.Property(faq => faq.Question)
            .HasColumnName("question")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(faq => faq.Answer)
            .HasColumnName("answer")
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(faq => faq.Category)
            .HasColumnName("category")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(faq => faq.Keywords)
            .HasColumnName("keywords")
            .HasMaxLength(500);

        builder.Property(faq => faq.DisplayOrder)
            .HasColumnName("display_order")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(faq => faq.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(faq => faq.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(faq => faq.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(faq => faq.Category);
        builder.HasIndex(faq => faq.IsActive);
        builder.HasIndex(faq => new { faq.IsActive, faq.Category, faq.DisplayOrder });
    }
}
