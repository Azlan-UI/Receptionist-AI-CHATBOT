using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class ChatSessionConfiguration : IEntityTypeConfiguration<ChatSession>
{
    public void Configure(EntityTypeBuilder<ChatSession> builder)
    {
        builder.ToTable("chat_sessions");

        builder.HasKey(session => session.Id);

        builder.Property(session => session.Id)
            .HasColumnName("id");

        builder.Property(session => session.UserId)
            .HasColumnName("user_id");

        builder.Property(session => session.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(session => session.Channel)
            .HasColumnName("channel")
            .HasMaxLength(50);

        builder.Property(session => session.VisitorIpAddress)
            .HasColumnName("visitor_ip_address")
            .HasMaxLength(45);

        builder.Property(session => session.Summary)
            .HasColumnName("summary")
            .HasMaxLength(2000);

        builder.Property(session => session.EndedAtUtc)
            .HasColumnName("ended_at_utc");

        builder.Property(session => session.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(session => session.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasOne(session => session.User)
            .WithMany(user => user.ChatSessions)
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(session => session.Messages)
            .WithOne(message => message.ChatSession)
            .HasForeignKey(message => message.ChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(session => session.UserId);
        builder.HasIndex(session => session.Status);
        builder.HasIndex(session => session.CreatedAtUtc);
        builder.HasIndex(session => new { session.UserId, session.CreatedAtUtc });
    }
}
