using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(message => message.Id);

        builder.Property(message => message.Id)
            .HasColumnName("id");

        builder.Property(message => message.ChatSessionId)
            .HasColumnName("chat_session_id")
            .IsRequired();

        builder.Property(message => message.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(message => message.Content)
            .HasColumnName("content")
            .HasMaxLength(8000)
            .IsRequired();

        builder.Property(message => message.IntentName)
            .HasColumnName("intent_name")
            .HasMaxLength(100);

        builder.Property(message => message.ConfidenceScore)
            .HasColumnName("confidence_score")
            .HasPrecision(5, 4);

        builder.Property(message => message.ResponseTime)
            .HasColumnName("response_time");

        builder.Property(message => message.IsHelpful)
            .HasColumnName("is_helpful")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(message => message.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(message => message.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasOne(message => message.ChatSession)
            .WithMany(session => session.Messages)
            .HasForeignKey(message => message.ChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(message => message.ChatSessionId);
        builder.HasIndex(message => message.CreatedAtUtc);
        builder.HasIndex(message => message.IntentName);
        builder.HasIndex(message => new { message.ChatSessionId, message.CreatedAtUtc });
    }
}
