using Azure.AI.OpenAI;
using M365Agent.Api.Agents;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var endpoint = builder.Configuration["Azure:OpenAIEndpoint"];
var apiKey = builder.Configuration["Azure:OpenAIApiKey"];
var deploymentName = builder.Configuration["Azure:OpenAIDeploymentName"];

var client = new AzureOpenAIClient(
    endpoint: new Uri(endpoint),
    credential: new System.ClientModel.ApiKeyCredential(apiKey))
    .GetChatClient(deploymentName)
    .AsIChatClient()
    .AsBuilder()
    .UseFunctionInvocation()
    .Build();

builder.Services.AddChatClient(client);

var agent = new LearnAgent(client);
await agent.InitializeAgent();

builder.Services.AddSingleton(agent);

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/agent/chat", async (
    [FromServices] LearnAgent learnAgent,
    string prompt) =>
{
    var response = await learnAgent.InvokeAgentAsync(prompt);
    return Results.Ok(response);
});


app.Run();