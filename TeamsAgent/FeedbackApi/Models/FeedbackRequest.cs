namespace FeedbackApi.Models;

public class FeedbackRequest
{
    public required string Reaction { get; set; }
    public required string Feedback { get; set; }
}
