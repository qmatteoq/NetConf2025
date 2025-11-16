namespace FeedbackApi.Models;

public class FeedbackEntry
{
    public int Id { get; set; }
    public required string Reaction { get; set; }
    public required string Feedback { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
