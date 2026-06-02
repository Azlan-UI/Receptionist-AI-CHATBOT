using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Application.Interfaces.Services;
using ReceptionistChatBot.Infrastructure.AI;
using ReceptionistChatBot.Infrastructure.Persistence;
using ReceptionistChatBot.Infrastructure.Repositories;
using ReceptionistChatBot.Infrastructure.Services;

namespace ReceptionistChatBot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddInfrastructure(connectionString);
        services.AddGemini(configuration);

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ReceptionistChatBotDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(ReceptionistChatBotDbContext).Assembly.FullName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IFaqService, FaqService>();
        services.AddScoped<ICompanyInformationService, CompanyInformationService>();
        services.AddScoped<IPromptBuilderService, PromptBuilderService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IChatSessionRepository, ChatSessionRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        services.AddScoped<ICompanyInformationRepository, CompanyInformationRepository>();
        services.AddScoped<IConversationSessionRepository, ConversationSessionRepository>();
        services.AddScoped<IFaqRepository, FaqRepository>();
        services.AddScoped<IKnowledgeBaseRepository, KnowledgeBaseRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddGemini(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<GeminiOptions>(
            configuration.GetSection(GeminiOptions.SectionName));

        services.AddHttpClient<IGeminiService, GeminiService>((serviceProvider, httpClient) =>
        {
            var options = serviceProvider
                .GetRequiredService<Microsoft.Extensions.Options.IOptions<GeminiOptions>>()
                .Value;

            httpClient.BaseAddress = new Uri(options.BaseUrl);
            httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        });

        return services;
    }
}
