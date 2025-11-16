using TeamsAgent;
using TeamsAgent.Controllers;
using Azure.Core;
using Azure.Identity;
using Microsoft.Teams.AI.Models.OpenAI;
using Microsoft.Teams.AI.Models.OpenAI.Extensions;
using Microsoft.Teams.AI.Prompts;
using Microsoft.Teams.Api.Auth;
using Microsoft.Teams.Apps;
using Microsoft.Teams.Apps.Extensions;
using Microsoft.Teams.Common.Http;
using Microsoft.Teams.Plugins.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var config = builder.Configuration.Get<ConfigOptions>();

if (config == null)
{
    throw new InvalidOperationException("Missing configuration for ConfigOptions");
}

Func<string[], string?, Task<ITokenResponse>> createTokenFactory = async (string[] scopes, string? tenantId) =>
{
    var clientId = config.Teams.ClientId;

    var managedIdentityCredential = new ManagedIdentityCredential(clientId);
    var tokenRequestContext = new TokenRequestContext(scopes, tenantId: tenantId);
    var accessToken = await managedIdentityCredential.GetTokenAsync(tokenRequestContext);

    return new TokenResponse
    {
        TokenType = "Bearer",
        AccessToken = accessToken.Token,
    };
};
var appBuilder = App.Builder();

if (config.Teams.BotType == "UserAssignedMsi")
{
    appBuilder.AddCredentials(new TokenCredentials(
        config.Teams.ClientId ?? string.Empty,
        async (tenantId, scopes) =>
        {
            return await createTokenFactory(scopes, tenantId);
        }
    ));
}

AzureAISearchDataSourceOptions options = new()
{
    IndexName = "my-documents",
    AzureAISearchApiKey = config.Azure.AISearchApiKey,
    AzureAISearchEndpoint = new Uri(config.Azure.AISearchEndpoint),
    AzureOpenAIApiKey = config.Azure.OpenAIApiKey,
    AzureOpenAIEndpoint = config.Azure.OpenAIEndpoint,
    AzureOpenAIEmbeddingDeployment = config.Azure.OpenAIEmbeddingDeploymentName,
};

AzureAISearchDataSource dataSource = new(options);
builder.Services.AddSingleton(new AzureAISearchDataSource(options));

builder.Services.AddSingleton<Controller>();
builder.AddTeams(appBuilder);

// Read instructions from file
var instructionsPath = Path.Combine(builder.Environment.ContentRootPath, "Prompts", "instructions.txt");
var instructions = await File.ReadAllTextAsync(instructionsPath);

builder.Services.AddOpenAI(
    new OpenAIChatModel(
        config.Azure.OpenAIDeploymentName, 
        config.Azure.OpenAIApiKey,
        new() { Endpoint = new Uri($"{config.Azure.OpenAIEndpoint}/openai/v1") }),
    new ChatPromptOptions().WithInstructions(instructions));

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseTeams();

app.Run();