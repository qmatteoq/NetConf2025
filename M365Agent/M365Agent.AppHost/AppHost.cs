var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.M365Agent_Api>("m365agent-api");

var app = builder.AddProject<Projects.M365Agent_App>("m365agent-app", launchProfileName: "Microsoft 365 Agents Playground")
    .WithReference(api);

// Add dev tunnel to expose the agent for M365 Agents Playground access
builder.AddDevTunnel("agent")
    .WithReference(app)
    .WithAnonymousAccess();

// Add agentsplayground command to run with the M365Agent.App URL
builder.AddExecutable("agentsplayground", "agentsplayground", workingDirectory: ".", args: [
    "-e", 
    ReferenceExpression.Create($"{app.GetEndpoint("http")}/api/messages"),
    "-c", 
    "emulator"
]);

builder.Build().Run();
