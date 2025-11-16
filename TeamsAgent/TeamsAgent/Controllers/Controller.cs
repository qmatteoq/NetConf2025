using TeamsAgent.Models;
using TeamsAgent.Utils;
using Microsoft.Teams.AI.Models.OpenAI;
using Microsoft.Teams.Api.Activities;
using Microsoft.Teams.Api.Activities.Invokes;
using Microsoft.Teams.Apps;
using Microsoft.Teams.Apps.Activities;
using Microsoft.Teams.Apps.Activities.Invokes;
using Microsoft.Teams.Apps.Annotations;
using System.Runtime.InteropServices;
using System.Net.Http;

namespace TeamsAgent.Controllers
{
    [TeamsController]
    public class Controller(OpenAIChatPrompt _prompt, AzureAISearchDataSource dataSource, IConfiguration configuration)
    {
        [Message]
        public async Task OnMessage(IContext<Microsoft.Teams.Api.Activities.MessageActivity> context)
        {
            var state = State.From(context);
            var text = TextUtils.StripMentionsText(context.Activity);
            
             var additionalContext = await dataSource.RenderDataAsync(text);
            var enrichedText = $"{text}\n\nAdditional Context: {additionalContext}";

            if (context.Activity.Conversation.IsGroup == true)
            {
                var response = await _prompt.Send(enrichedText, new() { Messages = state.Messages }, null, context.CancellationToken);
                await context.Send(new Microsoft.Teams.Api.Activities.MessageActivity(response.Content).AddFeedback().AddAIGenerated());
            }
            else
            {
                await _prompt.Send(enrichedText, new() { Messages = state.Messages }, (chunk) => Task.Run(() =>
                {
                    context.Stream.Emit(chunk);
                }), context.CancellationToken);

                context.Stream.Emit((Microsoft.Teams.Api.Activities.MessageActivity)new Microsoft.Teams.Api.Activities.MessageActivity().AddFeedback().AddAIGenerated());
            }
            state.Save(context);
        }

        [Microsoft.Teams.Apps.Activities.Invokes.Message.SubmitAction]
        public async Task OnSubmitAction(IContext<Messages.SubmitActionActivity> context)
        {
            var actionValue = context.Activity.Value.ActionValue.ToString() ?? string.Empty;
            Console.WriteLine($"Your feedback is {actionValue}");
            
            // Deserialize the JSON payload
            var feedbackData = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(actionValue);
            var reaction = feedbackData.GetProperty("reaction").GetString() ?? string.Empty;
            var feedbackJson = feedbackData.GetProperty("feedback").GetString() ?? string.Empty;
            
            // Parse the nested feedback JSON
            var feedbackElement = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(feedbackJson);
            var feedbackText = feedbackElement.GetProperty("feedbackText").GetString() ?? string.Empty;
            
            var url = $"{configuration["services:feedbackapi:http:0"]}";
            using HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            var feedbackRequest = new
            {
                Reaction = reaction,
                Feedback = feedbackText
            };

            await client.PostAsJsonAsync("/api/feedback", feedbackRequest);
        }

        [Conversation.MembersAdded]
        public async Task OnMembersAdded(IContext<ConversationUpdateActivity> context)
        {
            var welcomeText = "How can I help you today?";
            foreach (var member in context.Activity.MembersAdded)
            {
                if (member.Id != context.Activity.Recipient.Id)
                {
                    await context.Send(welcomeText);
                }
            }
        }
    }
}