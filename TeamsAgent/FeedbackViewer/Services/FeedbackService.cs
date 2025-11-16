using FeedbackViewer.Models;

namespace FeedbackViewer.Services;

public class FeedbackService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FeedbackService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<List<FeedbackEntry>> GetAllFeedbackAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("FeedbackApi");
        
        // Use service discovery URL
        var feedbackApiUrl = _configuration["services:feedbackapi:https:0"] 
                           ?? _configuration["services:feedbackapi:http:0"]
                           ?? "https://localhost:7001";

        try
        {
            var response = await httpClient.GetFromJsonAsync<List<FeedbackEntry>>($"{feedbackApiUrl}/api/feedback");
            return response ?? new List<FeedbackEntry>();
        }
        catch (Exception)
        {
            return new List<FeedbackEntry>();
        }
    }
}
