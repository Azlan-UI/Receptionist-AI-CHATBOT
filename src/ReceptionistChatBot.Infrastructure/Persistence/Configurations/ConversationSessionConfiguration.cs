using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Persistence.Configurations;

public sealed class ConversationSessionConfiguration : IEntityTypeConfiguration<ConversationSession>
{
    public void Configure(EntityTypeBuilder<ConversationSession> builder)
    {
        builder.ToTable("conversation_sessions");

        builder.HasKey(session => session.Id);

        builder.Property(session => session.Id).HasColumnName("id");

        builder.Property(session => session.PatientId)
            .HasColumnName("patient_id");

        builder.Property(session => session.Channel)
            .HasColumnName("channel")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(session => session.VisitorName)
            .HasColumnName("visitor_name")
            .HasMaxLength(150);

        builder.Property(session => session.VisitorContact)
            .HasColumnName("visitor_contact")
            .HasMaxLength(100);

        builder.Property(session => session.IsEscalatedToHuman)
            .HasColumnName("is_escalated_to_human")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(session => session.ClosedAtUtc)
            .HasColumnName("closed_at_utc");

        builder.Property(session => session.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(session => session.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(session => session.PatientId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(session => session.PatientId);
        builder.HasIndex(session => session.ClosedAtUtc);
        builder.HasIndex(session => session.IsEscalatedToHuman);
        builder.HasIndex(session => new { session.PatientId, session.CreatedAtUtc });
    }
}
