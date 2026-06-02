using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class KnowledgeBaseArticleConfiguration : IEntityTypeConfiguration<KnowledgeBaseArticle>
{
    public void Configure(EntityTypeBuilder<KnowledgeBaseArticle> builder)
    {
        builder.ToTable("knowledge_base_articles");

        builder.HasKey(article => article.Id);

        builder.Property(article => article.Id).HasColumnName("id");

        builder.Property(article => article.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(article => article.Content)
            .HasColumnName("content")
            .HasMaxLength(8000)
            .IsRequired();

        builder.Property(article => article.Category)
            .HasColumnName("category")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(article => article.IsPublished)
            .HasColumnName("is_published")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(article => article.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(article => article.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(article => article.Category);
        builder.HasIndex(article => article.IsPublished);
        builder.HasIndex(article => new { article.IsPublished, article.Category });
    }
}
