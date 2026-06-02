using ReceptionistChatBot.Web.Components;
using ReceptionistChatBot.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiBaseUrl = builder.Configuration["Api:BaseUrl"]
    ?? "https://localhost:7001";

builder.Services.AddHttpClient<ChatApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<FaqApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<CompanyInformationApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
