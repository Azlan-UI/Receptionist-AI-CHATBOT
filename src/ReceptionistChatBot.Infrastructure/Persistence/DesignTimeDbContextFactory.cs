using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReceptionistChatBot.Infrastructure.Persistence;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReceptionistChatBotDbContext>
{
    public ReceptionistChatBotDbContext CreateDbContext(string[] args)
    {
        const string connectionString =
            "Host=localhost;Port=5432;Database=receptionist_chatbot;Username=postgres;Password=azlan123";

        var optionsBuilder = new DbContextOptionsBuilder<ReceptionistChatBotDbContext>();
        optionsBuilder.UseNpgsql(
            connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(ReceptionistChatBotDbContext).Assembly.FullName));

        return new ReceptionistChatBotDbContext(optionsBuilder.Options);
    }
}
