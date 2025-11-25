using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace M365Agent.Api.Agents
{
    public class ReportAgent(IChatClient client)
    {
        private const string AgentName = "ReportAgent";
        private const string AgentInstructions = """
            You are a specialized assistant that helps users crafting detailed reports.
            You will see a series of information about Microsoft products, you must consolidate them into a single, detailed
            Markdown report.
            """;

        private ChatClientAgent _agent;
        private IChatClient _client = client;

        public async Task<AIAgent> InitializeAgent()
        {
            _agent = new ChatClientAgent
                (_client,
                name: AgentName,
                instructions: AgentInstructions);

            return _agent;
        }
    }
}
