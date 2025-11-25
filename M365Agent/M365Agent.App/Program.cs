using M365Agent.App;
using M365Agent.App.Bot;
using Microsoft.Agents.Hosting.AspNetCore;
using Microsoft.Agents.Builder.App;
using Microsoft.Agents.Builder;
using Microsoft.Agents.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
builder.Services.AddHttpContextAccessor();
builder.Logging.AddConsole();

// Register HttpClient for M365Agent.Api with Aspire service discovery
builder.Services.AddHttpClient("m365agent-api", client =>
{
    var serviceUrl = builder.Configuration["services:m365agent-api:http:0"];
    if (!string.IsNullOrEmpty(serviceUrl))
    {
        client.BaseAddress = new Uri(serviceUrl);
    }
    client.Timeout = TimeSpan.FromSeconds(600);
});

// Register the AI service of your choice. AzureOpenAI and OpenAI are demonstrated...
var config = builder.Configuration.Get<ConfigOptions>();

// Add AspNet token validation
builder.Services.AddBotAspNetAuthentication(builder.Configuration);

// Register IStorage.  For development, MemoryStorage is suitable.
// For production Agents, persisted storage should be used so
// that state survives Agent restarts, and operate correctly
// in a cluster of Agent instances.
builder.Services.AddSingleton<IStorage, MemoryStorage>();

// Add AgentApplicationOptions from config.
builder.AddAgentApplicationOptions();

// Add AgentApplicationOptions.  This will use DI'd services and IConfiguration for construction.
builder.Services.AddTransient<AgentApplicationOptions>();

// Add the bot (which is transient)
builder.AddAgent<LearnAgentBot>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/messages", async (HttpRequest request, HttpResponse response, IAgentHttpAdapter adapter, IAgent agent, CancellationToken cancellationToken) =>
{
    await adapter.ProcessAsync(request, response, agent, cancellationToken);
});

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Playground")
{
    app.MapGet("/", () => "Learn Agent Bot");
    app.UseDeveloperExceptionPage();
    app.MapControllers().AllowAnonymous();
}
else
{
    app.MapControllers();
}

app.Run();

