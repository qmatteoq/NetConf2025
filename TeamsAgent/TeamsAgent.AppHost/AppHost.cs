var builder = DistributedApplication.CreateBuilder(args);

var feedbackApi = builder.AddProject<Projects.FeedbackApi>("feedbackapi");

var feedbackViewer = builder.AddProject<Projects.FeedbackViewer>("feedbackviewer")
    .WithReference(feedbackApi);

var agent = builder.AddProject<Projects.TeamsAgent>("teamsagent", launchProfileName: "Start Project")
    .WithReference(feedbackApi);

builder.AddDevTunnel("agent")
    .WithReference(agent)
    .WithAnonymousAccess();

builder.Build().Run();
