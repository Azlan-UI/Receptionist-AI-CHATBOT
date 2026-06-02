using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Api.Middleware;
using ReceptionistChatBot.Infrastructure;
using ReceptionistChatBot.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed",
                Instance = context.HttpContext.Request.Path
            };

            return new BadRequestObjectResult(problemDetails);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ReceptionistChatBotDbContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    service = "ReceptionistChatBot API",
    version = "1.0.0",
    endpoints = new[]
    {
        "POST   /api/chat/send",
        "GET    /api/chat/history/{id}",
        "POST   /api/chat/session",
        "GET    /api/chat/sessions",
        "GET    /api/faqs",
        "POST   /api/faqs",
        "PUT    /api/faqs/{id}",
        "DELETE /api/faqs/{id}",
        "GET    /api/company-information/active",
        "PUT    /api/company-information"
    }
}));

app.Run();
