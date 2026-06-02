using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("chat_messages");

        builder.HasKey(message => message.Id);

        builder.Property(message => message.Id).HasColumnName("id");

        builder.Property(message => message.ConversationSessionId)
            .HasColumnName("conversation_session_id")
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

        builder.Property(message => message.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(message => message.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasOne<ConversationSession>()
            .WithMany()
            .HasForeignKey(message => message.ConversationSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(message => message.ConversationSessionId);
        builder.HasIndex(message => message.CreatedAtUtc);
        builder.HasIndex(message => message.IntentName);
        builder.HasIndex(message => new { message.ConversationSessionId, message.CreatedAtUtc });
    }
}
