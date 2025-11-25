using Azure.AI.OpenAI;
using M365Agent.Api.Agents;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Agents.AI.Workflows;
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

builder.AddAIAgent("LearnAgent", (sp, key) =>
{
    var chatClient = sp.GetRequiredService<IChatClient>();
    var learnAgent = new LearnAgent(chatClient);
    var agent = learnAgent.InitializeAgent().GetAwaiter().GetResult();
    return agent.AsBuilder().Build();
});

builder.AddAIAgent("Enterprise Knowledge Agent", (sp, key) =>
{
    var enterpriseAgent = new EnterpriseKnowledgeAgent(sp.GetRequiredService<IConfiguration>());
    var agent = enterpriseAgent.InitializeAgent().GetAwaiter().GetResult();
    return agent.AsBuilder().Build();
});

builder.AddAIAgent("ReportAgent", (sp, key) =>
{
    var chatClient = sp.GetRequiredService<IChatClient>();
    var reportAgent = new ReportAgent(chatClient);
    var agent = reportAgent.InitializeAgent().GetAwaiter().GetResult();
    return agent.AsBuilder().Build();
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/agent/chat", async (
    [FromKeyedServices("LearnAgent")] AIAgent learnAgent,
    [FromKeyedServices("Enterprise Knowledge Agent")] AIAgent enterpriseAgent,
    [FromKeyedServices("ReportAgent")] AIAgent reportAgent,
    string prompt,
    ILogger<Program> logger) =>
{
    logger.LogInformation("API invoked: /agent/chat");
    logger.LogInformation("Input parameter - Prompt: {Prompt}", prompt);

    var workflow = AgentWorkflowBuilder.BuildSequential(learnAgent, enterpriseAgent, reportAgent);
    var workflowAgent = workflow.AsAgent();
    var thread = workflowAgent.GetNewThread();
    var response = await workflowAgent.RunAsync(prompt);

    logger.LogInformation("Workflow completed. Total messages: {MessageCount}", response.Messages.Count);
    
    for (int i = 0; i < response.Messages.Count; i++)
    {
        var message = response.Messages[i];
        logger.LogInformation("Message {Index}: Role={Role}, Text={Text}", 
            i + 1, 
            message.Role, 
            message.Text);
    }

    var lastMessage = response.Messages.LastOrDefault();
    var output = lastMessage?.Text;
    
    logger.LogInformation("Output parameter - Response: {Response}", output);

    return Results.Ok(output);
});


app.Run();