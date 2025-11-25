using Microsoft.Agents.Builder;
using Microsoft.Agents.Builder.App;
using Microsoft.Agents.Builder.State;
using Microsoft.Agents.Core.Models;

namespace M365Agent.App.Bot;

public class LearnAgentBot : AgentApplication
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LearnAgentBot(AgentApplicationOptions options, IHttpClientFactory httpClientFactory) : base(options)
    {
        _httpClientFactory = httpClientFactory;
        OnConversationUpdate(ConversationUpdateEvents.MembersAdded, WelcomeMessageAsync);
        OnActivity(ActivityTypes.Message, MessageActivityAsync, rank: RouteRank.Last);
    }

    protected async Task MessageActivityAsync(ITurnContext turnContext, ITurnState turnState, CancellationToken cancellationToken)
    {
        // Start a Streaming Process 
        //await turnContext.StreamingResponse.QueueInformativeUpdateAsync("Working on a response for you");

        // Get the user's message
        var userMessage = turnContext.Activity.Text;

        // Call the M365Agent.Api using Aspire service discovery
        var httpClient = _httpClientFactory.CreateClient("m365agent-api");
        
        try
        {
            // Call the /agent/chat endpoint with the user's prompt
            var response = await httpClient.GetAsync($"/agent/chat?prompt={Uri.EscapeDataString(userMessage)}", cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var agentResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            await turnContext.SendActivityAsync(MessageFactory.Text(agentResponse));
            
            // Send the response back to the user
            //turnContext.StreamingResponse.QueueTextChunk(agentResponse);
        }
        catch (Exception ex)
        {
            // Handle errors gracefully
            turnContext.StreamingResponse.QueueTextChunk($"Sorry, I encountered an error: {ex.Message}");
        }

        //await turnContext.StreamingResponse.EndStreamAsync(cancellationToken); // End the streaming response
    }

    protected async Task WelcomeMessageAsync(ITurnContext turnContext, ITurnState turnState, CancellationToken cancellationToken)
    {
        foreach (ChannelAccount member in turnContext.Activity.MembersAdded)
        {
            if (member.Id != turnContext.Activity.Recipient.Id)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Hello and Welcome! I'm here to help with all your Microsoft Learn questions!"), cancellationToken);
            }
        }
    }
}