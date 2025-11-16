using Microsoft.EntityFrameworkCore;
using FeedbackApi.Data;
using FeedbackApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure SQLite database
builder.Services.AddDbContext<FeedbackDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FeedbackDb") ?? "Data Source=feedback.db"));

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FeedbackDbContext>();
    db.Database.EnsureCreated();
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// POST endpoint to submit feedback
app.MapPost("/api/feedback", async (FeedbackRequest request, FeedbackDbContext db) =>
{
    var feedbackEntry = new FeedbackEntry
    {
        Reaction = request.Reaction,
        Feedback = request.Feedback,
        CreatedAt = DateTime.UtcNow
    };

    db.Feedbacks.Add(feedbackEntry);
    await db.SaveChangesAsync();

    return Results.Created($"/api/feedback/{feedbackEntry.Id}", feedbackEntry);
})
.WithName("SubmitFeedback");

// GET endpoint to retrieve all feedback
app.MapGet("/api/feedback", async (FeedbackDbContext db) =>
{
    var feedbacks = await db.Feedbacks.OrderByDescending(f => f.CreatedAt).ToListAsync();
    return Results.Ok(feedbacks);
})
.WithName("GetAllFeedback");

// GET endpoint to retrieve feedback by ID
app.MapGet("/api/feedback/{id}", async (int id, FeedbackDbContext db) =>
{
    var feedback = await db.Feedbacks.FindAsync(id);
    return feedback is not null ? Results.Ok(feedback) : Results.NotFound();
})
.WithName("GetFeedbackById");

app.Run();
